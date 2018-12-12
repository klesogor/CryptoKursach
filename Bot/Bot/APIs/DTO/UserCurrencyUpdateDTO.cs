using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.APIs.DTO
{
    public class UserCurrencyUpdateDTO
    {
        public string Currency { get; set; }
        public string Market { get; set; }
        public decimal Rate { get; set; }
    }
}
