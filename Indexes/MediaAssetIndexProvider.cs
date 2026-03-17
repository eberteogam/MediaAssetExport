using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using YesSql.Indexes;

namespace TeoGamarra.MediaAssetExport.Indexes
{
    /// <summary>
    /// Index provider that extracts media asset URLs from content item JSON blobs.
    /// </summary>
    public class MediaAssetIndexProvider : IndexProvider<ContentItem>, IScopedIndexProvider
    {
        private readonly ILogger<MediaAssetIndexProvider> _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="MediaAssetIndexProvider"/>.
        /// </summary>
        public MediaAssetIndexProvider(ILogger<MediaAssetIndexProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Describes how <see cref="MediaAssetIndex"/> entries are mapped from <see cref="ContentItem"/> documents.
        /// </summary>
        public override void Describe(DescribeContext<ContentItem> context)
        {
            context.For<MediaAssetIndex>()
                .Map(contentItem =>
                {
                    var mediaAssets = ExtractMediaAssets(contentItem);

                    return mediaAssets.Select(url => new MediaAssetIndex
                    {
                        ContentItemId = contentItem.ContentItemId,
                        MediaUrl = url,
                        IsPublished = contentItem.Published,
                        ContentType = contentItem.ContentType
                    });
                });
        }

        /// <summary>
        /// Extracts distinct media asset URLs from a content item's JSON content.
        /// </summary>
        private List<string> ExtractMediaAssets(ContentItem contentItem)
        {
            var mediaUrls = new List<string>();

            try
            {
                var content = contentItem.Content as JsonElement?;

                if (content == null)
                    return mediaUrls;

                ExtractMediaUrlsFromJson(content.Value, mediaUrls);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to extract media assets from content item '{ContentItemId}'.", contentItem.ContentItemId);
            }

            return mediaUrls.Distinct().ToList();
        }

        /// <summary>
        /// Recursively extracts media URLs from a JSON element.
        /// </summary>
        private static void ExtractMediaUrlsFromJson(JsonElement element, List<string> mediaUrls)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name.Contains("url", System.StringComparison.OrdinalIgnoreCase) ||
                        property.Name.Contains("media", System.StringComparison.OrdinalIgnoreCase) ||
                        property.Name.Contains("path", System.StringComparison.OrdinalIgnoreCase))
                    {
                        if (property.Value.ValueKind == JsonValueKind.String)
                        {
                            var url = property.Value.GetString();
                            if (!string.IsNullOrEmpty(url))
                                mediaUrls.Add(url);
                        }
                    }

                    if (property.Value.ValueKind == JsonValueKind.Object ||
                        property.Value.ValueKind == JsonValueKind.Array)
                    {
                        ExtractMediaUrlsFromJson(property.Value, mediaUrls);
                    }
                }
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    ExtractMediaUrlsFromJson(item, mediaUrls);
                }
            }
        }
    }
}
