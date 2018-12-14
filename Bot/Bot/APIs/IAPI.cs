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
        Task<bool> AddSubscription(int userId, int currencyId, int marketId);
        Task<List<CurrencyRate>> GetCurrencyRate(int currencyId, int? marketId = null);
        Task<List<Currency>> GetAvailableCurrencies();
    }
}
