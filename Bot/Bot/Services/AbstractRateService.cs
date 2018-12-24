using Bot.APIs;
using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Bot.Entities;
using System.Text;

namespace Bot.Services
{
    public abstract class AbstractRateService : ApiService
    {
        public AbstractRateService(IAPI api) : base(api)
        {
        }

        public virtual IReply RenderRate(params CurrencyRate[] rates)
        {
            var builder = new StringBuilder();
            builder.Append("<pre>Most recent currency updates:</pre>");
            foreach (var rate in rates)
            {
                builder.Append($"Currency:<b>{rate.Currency.Name}</b>\n" +
                    $"Market: <b>{rate.Market.Name}</b>\n" +
                    $"Current rate: <b>{rate.Rate}</b>\n" +
                    $"Updated at: <b>{rate.UpdatedAt.ToShortTimeString()}</b>\n\n");
            }
            //remove trailing slashes
            builder.Remove(builder.Length - 2,1);
            return new Reply() { Text = builder.ToString() };
        }
    }
}
