using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeoGamarra.MediaAssetExport.Indexes;
using YesSql;

namespace TeoGamarra.MediaAssetExport.Services
{
    /// <summary>
    /// Service implementation for querying media assets using YeSQL.
    /// </summary>
    public class MediaAssetService : IMediaAssetService
    {
        private readonly ISession _session;

        public MediaAssetService(ISession session)
        {
            _session = session;
        }

        /// <summary>
        /// Gets distinct media asset URLs from all published content items.
        /// </summary>
        public async Task<List<string>> GetPublishedMediaAssetsAsync()
        {
            var indexes = await _session.QueryIndex<MediaAssetIndex>()
                .Where(x => x.IsPublished)
                .ListAsync();

            return indexes.Select(x => x.MediaUrl).Distinct().ToList();
        }

        /// <summary>
        /// Gets media asset URLs for a specific published content item.
        /// </summary>
        public async Task<List<string>> GetMediaAssetsByContentItemAsync(string contentItemId)
        {
            var indexes = await _session.QueryIndex<MediaAssetIndex>()
                .Where(x => x.ContentItemId == contentItemId && x.IsPublished)
                .ListAsync();

            return indexes.Select(x => x.MediaUrl).Distinct().ToList();
        }

        /// <summary>
        /// Gets media asset URLs for all published content items of a given content type.
        /// </summary>
        public async Task<List<string>> GetMediaAssetsByContentTypeAsync(string contentType)
        {
            var indexes = await _session.QueryIndex<MediaAssetIndex>()
                .Where(x => x.ContentType == contentType && x.IsPublished)
                .ListAsync();

            return indexes.Select(x => x.MediaUrl).Distinct().ToList();
        }
    }
}
