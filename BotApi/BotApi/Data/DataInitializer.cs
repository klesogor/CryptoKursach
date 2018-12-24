using BotApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext context;

        public DataInitializer(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            //if schema already exists - no seeds
            if (!context.Database.EnsureCreated()) return;

            var currencies = new Currency[] {
                    new Currency() { Name = "Bitcoin", Symbol = "Btc" },
                    new Currency() { Name = "Etherium", Symbol = "Eth" }
                };

            context.Currencies.AddRange(currencies);
            await context.SaveChangesAsync();

            var markets = new Market[] {
                new Market() {
                    Name = "Coin Market Cap",
                    AggregationDriver = Aggregators.AggregationDriverType.CoinMarketCapDriver
                }
            };

            context.Markets.AddRange(markets);
            await context.SaveChangesAsync();

            var marketCurrencies = new CurrencyMarket[] {
                new CurrencyMarket(){ Currency = currencies[0], Market = markets[0], MarketCurrencyId = 1 },
                new CurrencyMarket(){ Currency = currencies[1], Market = markets[0], MarketCurrencyId = 1027 }
            };

            context.CurrencyMarkets.AddRange(marketCurrencies);
            await context.SaveChangesAsync();

            var currencyRates = new CurrencyRate[] {
                new CurrencyRate(){ Currency = marketCurrencies[0], Date = DateTime.Now, Rate = 1.0M },
                new CurrencyRate(){ Currency = marketCurrencies[1], Date = DateTime.Now, Rate = 1.0M }
            };

            context.CurrencyRates.AddRange(currencyRates);
            await context.SaveChangesAsync();
        }
    }
}
