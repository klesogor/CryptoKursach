using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Entities
{
    public class CurrencyRate
    {
        public Currency Currency { get; set; }
        public decimal Rate { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
