using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeoGamarra.MediaAssetExport.Services
{
    /// <summary>
    /// Service for querying media assets from the YeSQL index.
    /// </summary>
    public interface IMediaAssetService
    {
        /// <summary>
        /// Gets distinct media asset URLs from all published content items.
        /// </summary>
        Task<List<string>> GetPublishedMediaAssetsAsync();

        /// <summary>
        /// Gets media asset URLs for a specific published content item.
        /// </summary>
        Task<List<string>> GetMediaAssetsByContentItemAsync(string contentItemId);

        /// <summary>
        /// Gets media asset URLs for all published content items of a given content type.
        /// </summary>
        Task<List<string>> GetMediaAssetsByContentTypeAsync(string contentType);
    }
}
