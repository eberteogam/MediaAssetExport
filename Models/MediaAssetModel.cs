namespace TeoGamarra.MediaAssetExport.Models
{
    /// <summary>
    /// Data model for a media asset entry.
    /// </summary>
    public class MediaAssetModel
    {
        /// <summary>
        /// Unique identifier for the media asset.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Content item ID that contains this media asset.
        /// </summary>
        public string ContentItemId { get; set; }

        /// <summary>
        /// Media asset URL.
        /// </summary>
        public string MediaUrl { get; set; }

        /// <summary>
        /// Whether the parent content item is published.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Content type of the parent item.
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// When the asset was indexed.
        /// </summary>
        public System.DateTime IndexedAt { get; set; } = System.DateTime.UtcNow;
    }
}
