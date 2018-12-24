using BotApi.Aggregators.Interfaces;
using BotApi.Data.Models;
using BotApi.Exceptions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BotApi.Aggregators.Drivers
{
    public class CointMarketCapDriver : IAggregationDriver
    {
        private const string _apiBaseUrl = "https://pro-api.coinmarketcap.com/v1/";
        private readonly HttpClient _httpClient;
        //no external dependencies, since we won't unit test external API driver
        public CointMarketCapDriver(IConfiguration config)
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(_apiBaseUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", config.GetSection("Secrets:CMCToken").Value);
        }

        public async Task<IEnumerable<CurrencyRate>> Aggreagate(IEnumerable<CurrencyMarket> CurrencyIds)
        {
            var data = await _httpClient.GetAsync("cryptocurrency/listings/latest?limit=200");
            if (!data.IsSuccessStatusCode) {
                throw new HttpException("Aggregation exception. Check logs for more details");
            }
            var response = await data.Content.ReadAsStringAsync();
            var date = DateTime.Now;
            var parser = JObject.Parse(response);
            var result = new List<CurrencyRate>();
            foreach (var token in parser["data"].Children())
            {
                var cm = CurrencyIds.FirstOrDefault(c => c.MarketCurrencyId == token.Value<int>("id"));
                if (cm != null) {
                    result.Add(new CurrencyRate() {
                        CurrencyId = cm.Id,
                        Date = date,
                        Rate = token["quote"]["USD"].Value<decimal>("price")
                    });
                }
            }

            return result;
        }
    }
}
