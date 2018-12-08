using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bot.APIs.DTO;

namespace Bot.APIs
{
    public class ApiMock : IAPI
    {
        public Task<SubscriptionDTO> AddSubscription(int userId, int currencyId, int marketId)
        {
            var DTO = new SubscriptionDTO { Success = true };

            return Task.Run(() => DTO);
        }

        public Task<CurrencyRateDTO> GetCurrencyRate(int currencyId)
        {
            var DTO = new CurrencyRateDTO() { Success = true, Currency = "Bitcoin", Rate = 100.20M };

            return Task.Run(() => DTO);
        }
    }
}
