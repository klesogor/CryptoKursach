﻿using BotApi.Data.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .HasIndex(s => new { s.CurrencyId, s.UserId })
                .IsUnique();
            modelBuilder.Entity<CurrencyMarket>()
                .HasIndex(cm => new { cm.CurrencyId, cm.MarketId })
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.ChatId)
                .IsUnique();
        }
    }
}
