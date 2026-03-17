using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeoGamarra.MediaAssetExport.Services;

namespace TeoGamarra.MediaAssetExport.Controllers
{
    /// <summary>
    /// API controller for media asset export operations.
    /// </summary>
    [ApiController]
    [Route("api/media-assets")]
    public class MediaAssetController : ControllerBase
    {
        private readonly IMediaAssetService _mediaAssetService;

        /// <summary>
        /// Initializes a new instance of <see cref="MediaAssetController"/>.
        /// </summary>
        public MediaAssetController(IMediaAssetService mediaAssetService)
        {
            _mediaAssetService = mediaAssetService;
        }

        /// <summary>
        /// Gets all media asset URLs from published content items.
        /// </summary>
        [HttpGet("published")]
        public async Task<ActionResult<List<string>>> GetPublishedMediaAssets()
        {
            var assets = await _mediaAssetService.GetPublishedMediaAssetsAsync();
            return Ok(assets);
        }

        /// <summary>
        /// Gets media asset URLs for a specific content item.
        /// </summary>
        [HttpGet("content-item/{contentItemId}")]
        public async Task<ActionResult<List<string>>> GetMediaAssetsByContentItem(string contentItemId)
        {
            var assets = await _mediaAssetService.GetMediaAssetsByContentItemAsync(contentItemId);
            return Ok(assets);
        }

        /// <summary>
        /// Gets media asset URLs for all published content items of a given content type.
        /// </summary>
        [HttpGet("content-type/{contentType}")]
        public async Task<ActionResult<List<string>>> GetMediaAssetsByContentType(string contentType)
        {
            var assets = await _mediaAssetService.GetMediaAssetsByContentTypeAsync(contentType);
            return Ok(assets);
        }
    }
}
