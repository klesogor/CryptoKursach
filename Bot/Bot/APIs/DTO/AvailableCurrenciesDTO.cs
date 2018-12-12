using Bot.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.APIs.DTO
{
    public class AvailableCurrenciesDTO
    {
        public IEnumerable<Currency> Currencies { get; set; }
    }
}
