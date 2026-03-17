using OrchardCore.Modules;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Data;
using TeoGamarra.MediaAssetExport.Indexes;
using TeoGamarra.MediaAssetExport.Services;

namespace TeoGamarra.MediaAssetExport
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScopedIndexProvider<MediaAssetIndexProvider>();
            services.AddScoped<IMediaAssetService, MediaAssetService>();
        }
    }
}
