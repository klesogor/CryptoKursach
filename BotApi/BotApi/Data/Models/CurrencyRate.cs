using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("currency_rates")]
    public class CurrencyRate: BaseWithIncrementsId
    {
        public int CurrencyId { get; set; }
        public virtual CurrencyMarket Currency { get; set; }

        public decimal Rate { get; set; }

        public DateTime Date { get; set; }
    }
}
