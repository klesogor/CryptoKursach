using Bot.APIs;
using Bot.APIs.DTO;
using Bot.Bot;
using Bot.Routers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Services
{
    public class RateService: ApiService
    {
        public RateService(IAPI api) : base(api) { }

        public IReply GetRate(ParameterBag bag)
        {
            int currencyId = int.Parse(bag.GetObjectAsString("currency_id"));
            var task = _api.GetCurrencyRate(currencyId);
            task.Wait();
            return new Reply() { Message = $"Current **{task.Result.Currency}** rate is *{task.Result.Rate.ToString("0.##")}*" };
        }

    }
}
