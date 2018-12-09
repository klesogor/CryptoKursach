using BotApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BotApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Market> Markets { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<CurrencyMarket> CurrencyMarkets { get; set; }

        public DbSet<CurrencyRate> CurrencyRates { get; set; }
    }
}
