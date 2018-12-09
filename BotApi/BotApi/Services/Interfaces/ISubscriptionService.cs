using BotApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Services.Interfaces
{
    public interface ISubscriptionService
    {
        Task<bool> Subscribe(int currencyId, int marketId, int chatId);

        Task<IEnumerable<Currency>> GetAvailableCurrencies();

        Task<IEnumerable<Market>> GetMarketsByCurrency(int currencyId);

        Task<IEnumerable<Subscription>> GetSubscriptionsByUser(int chatId);
    }
}
