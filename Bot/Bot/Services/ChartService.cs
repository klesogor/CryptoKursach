using Bot.APIs;
using Bot.Bot;
using Bot.Bot.Replies;
using Bot.Bot.Replies.Interfaces;
using Bot.Routers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Services
{
    class ChartService : ApiService
    {
        public ChartService(IAPI api):base(api)
        {

        }

        public async Task<IReply> GetAvailableChartCurrencies(ParameterBag bag, Chat chat)
        {
            var res = await _api.GetAvailableCurrencies();

            var keyboard = new List<InlineKeyboardButton>();

            foreach (var currency in res)
            {
                keyboard.Add(new InlineKeyboardButton()
                {
                    CallbackData = $"/chart{currency.Id}",
                    Text = currency.Name
                });
            }

            return new MenuReply()
            {
                Markup = new InlineKeyboardMarkup(keyboard),
                Text = "You can use keyboard below to show a chart of currencies" +
                "<b>Currencies list are:</b>"
            };

        }
    }
}
