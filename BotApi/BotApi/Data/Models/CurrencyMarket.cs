using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("currency_market")]
    public class CurrencyMarket: BaseWithIncrementsId
    {
        public virtual Currency Currency { get; set; }

        public virtual Market Market { get; set; }

        public int MarketCurrencyId { get; set; }
    }
}
