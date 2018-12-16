using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("subscriptions")]
    public class Subscription: BaseWithIncrementsId
    {
        public int CurrencyId { get; set; }
        public virtual CurrencyMarket Currency { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
