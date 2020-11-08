using LaundryManagerWeb.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LaundryManagerWeb.Startup))]
namespace LaundryManagerWeb
{
    public partial class Startup
    {
        public Startup()
        {

        }
        public void ConfigureServices (IServiceCollection service)
        {
            service.AddSingleton<ICacheManager, MemoryCacheManager>();

            //services.AddSingleton<ICacheManager>(x => new MemoryCacheManager(x.GetService<IMemoryCache>(), configuration));
        }
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }
}
