using OrchardCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data;
using TeoGamarra.MediaAssetExport.Indexes;
using TeoGamarra.MediaAssetExport.Services;

namespace TeoGamarra.MediaAssetExport
{
    /// <summary>
    /// Orchard Core module startup: registers the YeSQL index provider and media asset service.
    /// </summary>
    public class Startup : StartupBase
    {
        /// <summary>
        /// Registers module services with the dependency-injection container.
        /// </summary>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScopedIndexProvider<MediaAssetIndexProvider>();
            services.AddScoped<IMediaAssetService, MediaAssetService>();
        }
    }
}
