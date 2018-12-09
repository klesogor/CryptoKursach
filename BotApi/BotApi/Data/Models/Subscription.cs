using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("subscriptions")]
    public class Subscription: BaseWithIncrementsId
    {
        public virtual CurrencyMarket Currency { get; set; }

        public virtual User User { get; set; }
    }
}
