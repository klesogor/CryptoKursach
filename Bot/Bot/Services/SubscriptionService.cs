using Bot.APIs;
using Bot.APIs.DTO;
using Bot.Bot;
using Bot.Bot.Replies;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services
{
    public class SubscriptionService: ApiService
    {
        public SubscriptionService(IAPI api) : base(api) { }

        public IReply Subscribe(ParameterBag parameters, int chatId)
        {
            int currencyId = int.Parse(parameters.GetObjectAsString("currencyId"));
            int marketId = int.Parse(parameters.GetObjectAsString("marketId"));

            var task = _api.Subscribe(chatId, currencyId, marketId);
            task.Wait();

            return new Reply() { Text = "You successfully subscribed to rate updates! You cat stop receiiving them by unsibscribing" };
        }

        public IReply GetAvailableCurrencies(ParameterBag bag, int chatId)
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

        public IReply GetAwailableMarketsByCurrency(ParameterBag bag, int chatId)
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
