using BotApi.Data.Models;

namespace BotApi.DTO
{
    public class SubscriptionDTO
    {
        public CurrencyDTO Currency { get;set; }

        public MarketDTO Market { get; set; }

        public int Id { get; set; }
    }
}
