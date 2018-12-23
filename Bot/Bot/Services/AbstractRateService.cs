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
            foreach (var rate in rates)
            {
                builder.Append($"Currency:<pre>{rate.Currency.Name}</pre>\n" +
                    $"Market: <pre>{rate.Market.Name}</pre>\n" +
                    $"Current rate: <pre>{rate.Rate}</pre>\n" +
                    $"Updated at: <pre>{rate.UpdatedAt.ToShortTimeString()}</pre>\n\n");
            }
            //remove last slashes
            builder.Remove(builder.Length - 3,2);
            return new Reply() { Text = builder.ToString() };
        }
    }
}
