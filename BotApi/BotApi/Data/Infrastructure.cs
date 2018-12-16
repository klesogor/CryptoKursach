using BotApi.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BotApi.Data
{
    public static class DAModule
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services
                .AddDbContext<ApplicationDbContext>(options => {
                    options.UseLazyLoadingProxies();
                    options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
                    }
                );

            services.AddTransient<DataInitializer>();

            services.AddScoped(typeof(DbContext), typeof(ApplicationDbContext));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
