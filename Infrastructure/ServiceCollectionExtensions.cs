using Application.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services,IConfiguration configuration) {
            return
                    services.AddTransient<IPropertyRepo, PropertyRepo>()
                    .AddTransient<IImageRepo, ImageRepo>()
                    .AddTransient<ICountryRepo, CountryRepo>()
                    .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
        }
    }
}
