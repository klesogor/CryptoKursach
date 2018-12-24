using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.Entities;

namespace Bot.APIs
{
    public class ApiCacher : IAPI
    {
        private readonly MemoryCache _cache;
        private readonly IAPI _api;
        private readonly string currencyListKey = "temp";
        private readonly int _cacheTTL;

        public ApiCacher(IAPI api, int cacheLifetime = 5 * 60 * 1000)
        {
            _api = api;
            _cacheTTL = cacheLifetime;

            var options = new MemoryCacheOptions();
            _cache = new MemoryCache(options);
        }

        public Task Subscribe(int userId, int currencyId, int marketId)
        {
            return _api.Subscribe(userId, currencyId, marketId);
        }

        public Task<List<Currency>> GetAvailableCurrencies()
        {
            return _cache.GetOrCreateAsync(currencyListKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = new TimeSpan(_cacheTTL);
                return _api.GetAvailableCurrencies();
            });
        }

        public Task<List<Market>> GetAvailableMarkets(int currencyId)
        {
            return _cache.GetOrCreateAsync($"markets-{currencyId}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = new TimeSpan(_cacheTTL);
                return _api.GetAvailableMarkets(currencyId);
            });
        }

        public Task<CurrencyRate> GetCurrencyRate(int currencyId, int marketId)
        {
            return _cache.GetOrCreateAsync($"rate-{currencyId}:{marketId}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = new TimeSpan(_cacheTTL);
                return _api.GetCurrencyRate(currencyId, marketId);
            });
        }

        public  Task Start(int userId, string userName)
        {
            return _api.Start(userId,userName);
        }

        public Task<List<Subscription>> GetSubscriptions(int chatId)
        {
            return _api.GetSubscriptions(chatId);
        }

        public Task Unsubscribe(int userId, int currencyId)
        {
            return _api.Unsubscribe(userId, currencyId);
        }

        public Task<List<RateUpdate>> AggregateUpdates()
        {
            return _api.AggregateUpdates();
        }
    }
}