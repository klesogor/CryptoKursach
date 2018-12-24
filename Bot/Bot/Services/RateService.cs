using Bot.APIs;
using Bot.APIs.DTO;
using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Bot.Services
{
    public class RateService: AbstractRateService
    {
        public RateService(IAPI api) : base(api) { }

        public async Task<IReply> GetRate(ParameterBag bag, Chat chat)
        {
            int currencyId = int.Parse(bag.GetObjectAsString("currencyId"));
            int marketId = int.Parse(bag.GetObjectAsString("marketId"));
            var res = await _api.GetCurrencyRate(currencyId,marketId);
            return RenderRate(res);
        }

    }
}
