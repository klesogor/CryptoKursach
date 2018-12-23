using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.DTO
{
    public class CurrencyRateDTO
    {
        public MarketDTO Market { get; set; }

        public CurrencyDTO Currency { get; set; }

        public decimal Rate { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
