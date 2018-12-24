using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("currency_market")]
    public class CurrencyMarket: BaseWithIncrementsId
    {
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        public virtual List<CurrencyRate> Rates { get; set; }

        public int MarketCurrencyId { get; set; }
    }
}
