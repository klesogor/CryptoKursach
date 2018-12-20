using Bot.APIs;
using Bot.Bot;
using Bot.Bot.Replies;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services
{
    public class SubscriptionService: ApiService
    {
        public SubscriptionService(IAPI api) : base(api) { }

        public IReply Subscribe(ParameterBag parameters, Chat chat)
        {
            int currencyId = int.Parse(parameters.GetObjectAsString("currencyId"));
            int marketId = int.Parse(parameters.GetObjectAsString("marketId"));

            var task = _api.Subscribe((int)chat.Id, currencyId, marketId);
            task.Wait();
            if (task.Result) { 
                return new Reply() { Text = "You successfully subscribed for rate updates! You can stop receiving them by unsibscribing" };
            }

            return new Reply() { Text = "You already subscribed for currency rate updates!" };
        }

        public IReply GetAvailableCurrencies(ParameterBag bag, Chat chat)
        {
            try
            {
                var task = _api.GetAvailableCurrencies();
                task.Wait();
                var result = task.Result;

                var keyboard = new List<InlineKeyboardButton>();

                foreach (var currency in result)
                {
                    keyboard.Add(new InlineKeyboardButton() {
                        CallbackData = $"/subscribe {currency.Id}",
                        Text = currency.Name
                    });
                }

                return new MenuReply() {
                    Markup = new InlineKeyboardMarkup(keyboard),
                    Text = "<b>Currenies list:</b>"
                };
            }
            catch (Exception ex) {
                Console.Write(ex.Message);
                throw ex;
            }
        }

        public IReply GetAwailableMarketsByCurrency(ParameterBag bag, Chat chat)
        {
            try
            {
                int currencyId = int.Parse(bag.GetObjectAsString("currencyId"));
                var task = _api.GetAvailableMarkets(currencyId);
                task.Wait();
                var res = task.Result;

                var keyboard = new List<InlineKeyboardButton>();

                foreach (var market in res)
                {
                    keyboard.Add(new InlineKeyboardButton()
                    {
                        CallbackData = $"{bag.GetObjectAsString("command")} {market.Id}",
                        Text = market.Name
                    });
                }

                return new MenuReply()
                {
                    Markup = new InlineKeyboardMarkup(keyboard),
                    Text = "<b>Markets list:</b>"
                };
            }
            catch (Exception ex) { 
                Console.Write(ex.Message);
                throw ex;
            }
        }
    }
}
