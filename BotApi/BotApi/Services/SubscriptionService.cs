using BotApi.Data;
using BotApi.Data.Models;
using BotApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly ApplicationDbContext _db;

        public SubscriptionService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Subscribe(int currencyId, int marketId, int chatId)
        {
            try {
                _db.Subscriptions.Add(
                        new Subscription()
                        {
                            Currency = _db.CurrencyMarkets
                                .SingleOrDefault(e => e.Currency.Id == currencyId && e.Market.Id == marketId),
                            User = _db.Users.SingleOrDefault(e => e.ChatId == chatId)
                        }
                    );
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public Task<IEnumerable<Currency>> GetAvailableCurrencies()
        {
            return Task.Run(() => (IEnumerable<Currency>)_db.Currencies.ToList());
        }

        public Task<IEnumerable<Market>> GetMarketsByCurrency(int currencyId)
        {
            return Task.Run(() => (IEnumerable<Market>)_db.CurrencyMarkets
                    .Where(x => x.Currency.Id == currencyId)
                    .Select(x => x.Market)
                    .ToList()
                );
        }

        public Task<IEnumerable<Subscription>> GetSubscriptionsByUser(int chatId)
        {
            return Task.Run(() => (IEnumerable<Subscription>)_db.Users.SingleOrDefault(x => x.ChatId == chatId).Subscriptions.ToList());
        }
    }
}
