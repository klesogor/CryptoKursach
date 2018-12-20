using BotApi.Data.Models;

namespace BotApi.DTO
{
    public class SubscriptionDTO
    {
        public Currency Currency {get;set;}

        public Market Market { get; set; }

        public int SubscriptionId { get; set; }
    }
}
