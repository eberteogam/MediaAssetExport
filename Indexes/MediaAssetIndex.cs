using YesSql.Indexes;

namespace TeoGamarra.MediaAssetExport.Indexes
{
    /// <summary>
    /// YeSQL index for media assets extracted from published content items.
    /// </summary>
    public class MediaAssetIndex : MapIndex
    {
        /// <summary>
        /// Content item ID that contains this media asset.
        /// </summary>
        public required string ContentItemId { get; set; }

        /// <summary>
        /// Media asset URL reference.
        /// </summary>
        public required string MediaUrl { get; set; }

        /// <summary>
        /// Whether the parent content item is published.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Content type of the parent item.
        /// </summary>
        public required string ContentType { get; set; }
    }
}
