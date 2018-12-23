using Bot.APIs;
using Bot.APIs.DTO;
using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Services
{
    public class RateService: AbstractRateService
    {
        public RateService(IAPI api) : base(api) { }

        public IReply GetRate(ParameterBag bag)
        {
            int currencyId = int.Parse(bag.GetObjectAsString("currency_id"));
            var task = _api.GetCurrencyRate(currencyId);
            task.Wait();
            throw new NotImplementedException();
        }

    }
}
