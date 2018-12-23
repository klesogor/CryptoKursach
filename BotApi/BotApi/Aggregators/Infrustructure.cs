using BotApi.Aggregators;
using BotApi.Aggregators.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotApi.Services
{
    public static class AggregationModule
    {
        public static void ConfigureDrivers(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IDriverFactory), typeof(AggregationDriverFactory));
        }
    }
}
