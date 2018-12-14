using System;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Bot.APIs.DTO;
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

        public Task<bool> AddSubscription(int userId, int currencyId, int marketId)
        {
            return _api.AddSubscription(userId, currencyId, marketId);
        }

        public Task<List<Currency>> GetAvailableCurrencies()
        {
            return _cache.GetOrCreateAsync(currencyListKey, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = new TimeSpan(_cacheTTL);
                return _api.GetAvailableCurrencies();
            });
        }

        public Task<List<CurrencyRate>> GetCurrencyRate(int currencyId, int? marketId = null)
        {
            return _cache.GetOrCreateAsync($"{currencyId.ToString()}: {marketId?.ToString()}", (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = new TimeSpan(_cacheTTL);
                return _api.GetCurrencyRate(currencyId, marketId);
            });
        }
    }
}
