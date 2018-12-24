using BotApi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BotApi.Services
{
    public static class ServicesModule
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(ISubscriptionService), typeof(SubscriptionService));
            services.AddScoped(typeof(IUserService), typeof(UserService));
            services.AddScoped(typeof(IAggregationService), typeof(AggregationService));
        }
    }
}
