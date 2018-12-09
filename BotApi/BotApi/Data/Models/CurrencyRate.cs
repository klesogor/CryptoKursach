using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotApi.Data.Models
{
    [Table("currency_rates")]
    public class CurrencyRate: BaseWithIncrementsId
    {
        public virtual CurrencyMarket Currency { get; set; }

        public decimal Rate { get; set; }

        public DateTime Date { get; set; }

        public int Batch { get; set; }
    }
}
