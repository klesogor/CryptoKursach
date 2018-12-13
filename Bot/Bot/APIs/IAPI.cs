using Bot.APIs.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bot.APIs
{
    public interface IAPI
    {
        Task<SubscriptionDTO> AddSubscription(int userId, int currencyId, int marketId);
        Task<CurrencyRateDTO> GetCurrencyRate(int currencyId, int? marketId = null);
        Task<AvailableCurrenciesDTO> GetAvailableCurrencies();
    }
}
