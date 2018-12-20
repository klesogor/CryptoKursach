using Bot.APIs.DTO;
using Bot.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bot.APIs
{
    public interface IAPI
    {
        Task Subscribe(int userId, int currencyId, int marketId);
        Task Unsubscribe(int userId, int currencyId);
        Task<List<Currency>> GetAvailableCurrencies();
        Task<List<Market>> GetAvailableMarkets(int currencyId);
        Task<List<CurrencyRate>> GetCurrencyRate(int currencyId, int? marketId = null);
        Task Start(int userId, string userName);
        Task<List<Subscription>> GetSubscriptions(int chatId);
    }
}
