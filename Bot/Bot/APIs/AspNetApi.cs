using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Bot.APIs.DTO;
using Bot.Entities;
using Bot.Exceptions;
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
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(resultString);
            }

            return JsonConvert.DeserializeObject<List<Currency>>(resultString);
        }

        public async Task<List<Market>> GetAvailableMarkets(int currencyId)
        {
            var result = await _http.GetAsync($"currency/{currencyId}/markets");
            var resultString = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(resultString);
            }

            return JsonConvert.DeserializeObject<List<Market>>(resultString);
        }

        public async Task<CurrencyRate> GetCurrencyRate(int currencyId, int marketId)
        {
            var result = await _http.GetAsync($"currency/rate/{currencyId}/{marketId}");
            var resultString = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(resultString);
            }

            return JsonConvert.DeserializeObject<CurrencyRate>(resultString);
        }

        public async Task<List<Subscription>> GetSubscriptions(int chatId)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://localhost:64132/api/v1/subscription"),
                Headers = {
                { "X-CHAT-ID", chatId.ToString() }
            },
            };

            var result = await _http.SendAsync(httpRequestMessage);
            var resultString = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(resultString);
            }

            return JsonConvert.DeserializeObject<List<Subscription>>(resultString);
        }

        public async Task Start(int userId, string userName)
        {
            var data = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "ChatId", userId.ToString() },
                { "Name", userName.ToString() }
            });

            var result = await _http.PostAsync("start", data);
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task Subscribe(int userId, int currencyId, int marketId)
        {
            var data = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "CurrencyId", currencyId.ToString() },
                { "MarketId", marketId.ToString() }
            });
            data.Headers.Add("X-CHAT-ID", new[] { userId.ToString() });

            var result = await _http.PostAsync("subscribe", data);
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task Unsubscribe(int userId, int currencyId)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"http://localhost:64132/api/v1/subscription/{currencyId}"),
                Headers = {
                { "X-CHAT-ID",userId.ToString() }
            },
            };

            var result = await _http.SendAsync(httpRequestMessage);
            var resultString = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(resultString);
            }
        }

        public async Task<List<RateUpdate>> AggregateUpdates()
        {
            var result = await _http.PostAsync("aggregate",null);
            var resultString = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApiException(resultString);
            }

            return JsonConvert.DeserializeObject<List<RateUpdate>>(resultString);
        }
    }
}
