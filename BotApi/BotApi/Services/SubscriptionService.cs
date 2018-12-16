using BotApi.Data.DAL;
using BotApi.Data.Models;
using BotApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Services
{
    public class SubscriptionService: ISubscriptionService
    {
        private readonly IUnitOfWork UOW;

        public SubscriptionService(IUnitOfWork uow)
        {
            UOW = uow;
        }

        public async Task<bool> Subscribe(int currencyId, int marketId, int chatId)
        {
            var subscriptionRepo = UOW.GetRepository<Subscription>();
            

            var res = await subscriptionRepo.CreateAsync(new Subscription()
                {
                    Currency = await GetCurrencyMarket(marketId,currencyId),
                    User = await GetUserByChatId(chatId)
                }
            );

            await UOW.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<Currency>> GetAvailableCurrencies()
        {
            return await UOW.GetRepository<Currency>().
                GetAllAsync(c => c.CurrencyMarkets.Count > 0);
        }

        public async Task<IEnumerable<Market>> GetMarketsByCurrency(int currencyId)
        {
            return (
                    await UOW.GetRepository<CurrencyMarket>()
                    .GetAllAsync(cm => cm.Currency.Id == currencyId)
                 )
                 .Select(cm => cm.Market);
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptionsByUser(int chatId)
        {
            var user = await GetUserByChatId(chatId);

            return await UOW.GetRepository<Subscription>().GetAllAsync(s => s.User.Id == user.Id);
        }

        public async Task<bool> Unsubscribe(int currencyId, int marketId, int chatId)
        {
            var user = GetUserByChatId(chatId);
            var currency = GetCurrencyMarket(marketId, currencyId);

            var repo = UOW.GetRepository<Subscription>();
            var sub = (await repo.GetAllAsync(s => s.Currency.Id == currency.Id && s.User.Id == user.Id)).First();
            await Unsibscribe(sub.Id);

            return true;
        }

        public async Task<bool> Unsibscribe(int subscriptionId)
        {
            await UOW.GetRepository<Subscription>().DeleteAsync(subscriptionId);
            await UOW.SaveAsync();

            return true;
        }

        private async Task<User> GetUserByChatId(int chatId)
        {
            return (
                await UOW.GetRepository<User>()
                .GetAllAsync(u => u.ChatId == chatId)
            ).First();
        }

        private async Task<CurrencyMarket> GetCurrencyMarket(int marketId, int currencyId)
        {
            return (
                await UOW.GetRepository<CurrencyMarket>()
                    .GetAllAsync(cm => 
                        cm.Market.Id == marketId 
                        && cm.Currency.Id == currencyId
                    )
                ).First();
        }
    }
}
