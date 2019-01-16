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
    public class ChartService : ApiService
    {
        public ChartService(IAPI api) : base(api)
        {

        }

        public async Task<IReply> GetCurrencyListForCharts(ParameterBag bag, Chat chat)
        {
            var res = await _api.GetSubscriptions((int)chat.Id);

            var text = "This is your subscriptions. You can use keyboard below to check rate of any currency.\n" +
                "<b>Your current subscriptions are:</b>\n";
            text += string.Join('\n', res.Select(s => $" - <b>{s.Currency.Symbol}</b>@<b>{s.Market.Name}</b>"));
            var keyboard = new List<InlineKeyboardButton>();

            foreach (var subscription in res)
            {
                keyboard.Add(new InlineKeyboardButton()
                {
                    CallbackData = $"/chart {subscription.Currency.Id} {subscription.Market.Id}",
                    Text = $"{subscription.Currency.Symbol}@{subscription.Market.Name}"
                });
            }
            return new MenuReply()
            {
                Markup = new InlineKeyboardMarkup(keyboard),
                Text = text
            };
        }

        public async Task<IReply> GetChart(ParameterBag bag, Chat chat)
        {
            var res = await _api.GetChart(
                    int.Parse(bag.GetObjectAsString("currencyId")),
                    int.Parse(bag.GetObjectAsString("marketId"))
                );

            return new ImageReply() {
                Caption = $"Latest chart for {res.Currency.Name} at {res.Market.Name}",
                ImageUrl = res.ChartUrl
            };
        }
    }
}
