using Bot.APIs;
using Bot.Bot;
using Bot.Bot.Replies;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services
{
    public class SubscriptionService: ApiService
    {
        public SubscriptionService(IAPI api) : base(api) { }

        public async Task<IReply> Subscribe(ParameterBag parameters, Chat chat)
        {
            int currencyId = int.Parse(parameters.GetObjectAsString("currencyId"));
            int marketId = int.Parse(parameters.GetObjectAsString("marketId"));

            await _api.Subscribe((int)chat.Id, currencyId, marketId);
 

            return new Reply() { Text = "You successfully subscribed for rate updates! You can stop receiving them by unsibscribing" };

        }

        public async Task<IReply> GetAvailableCurrencies(ParameterBag bag, Chat chat)
        {
                var res = await _api.GetAvailableCurrencies();

                var keyboard = new List<InlineKeyboardButton>();

                foreach (var currency in res)
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

        public async Task<IReply> GetAwailableMarketsByCurrency(ParameterBag bag, Chat chat)
        {
                int currencyId = int.Parse(bag.GetObjectAsString("currencyId"));
                var res = await _api.GetAvailableMarkets(currencyId);

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

        public async Task<IReply> GetSubscriptions(ParameterBag bag, Chat chat)
        {
            var res = await _api.GetSubscriptions((int)chat.Id);

            var text = "This is your subscriptions. You can use keyboard below to check rate of any currency.\n" +
                "<b>Your current subscriptions are:</b>\n";
            text += string.Join('\n',res.Select(s => $" - <b>{s.Currency.Symbol}</b>@<b>{s.Market.Name}</b>"));
            var keyboard = new List<InlineKeyboardButton>();

            foreach (var subscription in res)
            {
                keyboard.Add(new InlineKeyboardButton()
                {
                    CallbackData = $"/rate {subscription.Currency.Id} {subscription.Market.Id}",
                    Text = $"{subscription.Currency.Symbol}@{subscription.Market.Name}"
                });
            }

            return new MenuReply()
            {
                Markup = new InlineKeyboardMarkup(keyboard),
                Text = text
            };
        }

        public async Task<IReply> GetSubscriptionsForUnsubscribe(ParameterBag bag, Chat chat)
        {
            var res = await _api.GetSubscriptions((int)chat.Id);

            var text = "This is your subscriptions. You can use keyboard below to unsubscribe.\n" +
                "<b>Your current subscriptions are:</b>\n";
            text += string.Join('\n', res.Select(s => $" - <b>{s.Currency.Symbol}</b>@<b>{s.Currency.Name}</b>"));
            var keyboard = new List<InlineKeyboardButton>();

            foreach (var subscription in res)
            {
                keyboard.Add(new InlineKeyboardButton()
                {
                    CallbackData = $"/unsubscribe {subscription.Id}",
                    Text = $"{subscription.Currency.Symbol}@{subscription.Market.Name}"
                });
            }

            return new MenuReply()
            {
                Markup = new InlineKeyboardMarkup(keyboard),
                Text = text
            };
        }

        public async Task<IReply> Unsubscribe(ParameterBag bag, Chat chat)
        {
            await _api.Unsubscribe((int)chat.Id, int.Parse(bag.GetObjectAsString("currencyId")));
            return new Reply() { Text = "Unsubscribed successfully. Now you will not recive notifications" };
        }
    }
}
