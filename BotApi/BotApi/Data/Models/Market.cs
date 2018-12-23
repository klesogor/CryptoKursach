using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BotApi.Aggregators;

namespace BotApi.Data.Models
{
    [Table("markets")]
    public class Market: BaseWithIncrementsId
    {
        [MaxLength(256)]
        public string Name { get; set; }

        public AggregationDriverType AggregationDriver { get; set; }

        public virtual List<CurrencyMarket> Currencies { get; set; }
    }
}
