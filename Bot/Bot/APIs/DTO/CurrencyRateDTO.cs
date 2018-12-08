using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.APIs.DTO
{
    public class CurrencyRateDTO: DTO
    {
        public string Currency { get; set; }
        public decimal Rate { get; set; }
    }
}
