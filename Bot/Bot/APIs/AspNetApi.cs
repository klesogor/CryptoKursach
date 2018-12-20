using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bot.Entities;
using Newtonsoft.Json;

namespace Bot.APIs
{
    public class AspNetApi : IAPI
    {
        private const string key = "c2VjcmV0IGtleQ==";
        //incapsulate dependencies. No real need for DI, since this is .NET objects
        private readonly HttpClient _http;

        public AspNetApi()
        {
            _http = new HttpClient() {BaseAddress = new Uri("http://localhost:64132/api/v1/") };
            _http.DefaultRequestHeaders.Add("X-AUTH-TOKEN", key);
        }

        public async Task<List<Currency>> GetAvailableCurrencies()
        {
            var result = await _http.GetAsync("currencies");
            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Currency>>(resultString);
        }

        public async Task<List<Market>> GetAvailableMarkets(int currencyId)
        {
            var result = await _http.GetAsync($"currency/{currencyId}/markets");
            var resultString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Market>>(resultString);
        }

        public Task<List<CurrencyRate>> GetCurrencyRate(int currencyId, int? marketId = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Start(int userId, string userName)
        {
            var data = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "ChatId", userId.ToString() },
                { "Name", userName.ToString() }
            });

            var result = await _http.PostAsync("start", data);
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> Subscribe(int userId, int currencyId, int marketId)
        {
            var data = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "CurrencyId", currencyId.ToString() },
                { "MarketId", marketId.ToString() }
            });
            data.Headers.Add("X-CHAT-ID", new[] { userId.ToString() });

            var result = await _http.PostAsync("subscribe", data);
            var resdata = await result.Content.ReadAsStringAsync();
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
