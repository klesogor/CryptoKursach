using System;

namespace Bot.Entities
{
    public class CurrencyRate
    {
        public Currency Currency { get; set; }
        public decimal Rate { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Market Market { get; set; }
    }
}
