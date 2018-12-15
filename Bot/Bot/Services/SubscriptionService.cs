using Bot.APIs;
using Bot.APIs.DTO;
using Bot.Bot;
using Bot.Routers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bot.Services
{
    public class SubscriptionService: ApiService
    {
        public SubscriptionService(IAPI api) : base(api) { }

        public IReply Subscribe(ParameterBag parameters)
        {
            int userId = int.Parse(parameters.GetObjectAsString("user_id"));
            int currencyId = int.Parse(parameters.GetObjectAsString("currency_id"));
            int marketId = int.Parse(parameters.GetObjectAsString("market_id"));

            var task = _api.AddSubscription(userId, currencyId, marketId);

            task.Wait();
            return new Reply() {Message = "Subscribed successfully, you will now recive currency rate updates" };
        }

        public IReply GetAvailableCurrencies(ParameterBag bag)
        {
            try
            {
                var task = _api.GetAvailableCurrencies();
                task.Wait();
                var result = task.Result;

                var courses = result.Select(x => $"* *{x.Name}*").ToArray();
                return new Reply() { Message = $"Available currencies are: \n {string.Join(';',courses)}" };
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                return new Reply() { Message = "Something went wrong" };
            }
        }
    }
}
