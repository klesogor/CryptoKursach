using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bot.APIs.DTO;
using Newtonsoft.Json;

namespace Bot.APIs
{
    public class AspNetApi : IAPI
    {
        private static readonly string _baseUrl = "http://localhost:64132/api";
        //incapsulate dependencies. No real need for DI, since this is .NET objects
        private readonly HttpClient _http = new HttpClient();

        public Task<SubscriptionDTO> AddSubscription(int userId, int currencyId, int marketId)
        {
            throw new NotImplementedException();
        }

        public async Task<AvailableCurrenciesDTO> GetAvailableCurrencies()
        {
            var result = await _http.GetAsync($"{_baseUrl}/subscription");
            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<AvailableCurrenciesDTO>>(resultString).Value;
        }

        public Task<CurrencyRateDTO> GetCurrencyRate(int currencyId, int? marketId)
        {
            throw new NotImplementedException();
        }
    }
}
