using Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static CacheSettings GetCacheSettings(this IServiceCollection services,IConfiguration configuration)
        {
            var cacheSettingConfigurations = configuration.GetSection("CacheSettings");

            services.Configure<CacheSettings>(cacheSettingConfigurations);
            
            return cacheSettingConfigurations.Get<CacheSettings>();
        }
    }
}
