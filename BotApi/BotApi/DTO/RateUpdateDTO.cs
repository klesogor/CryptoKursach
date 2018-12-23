using System.Collections.Generic;

namespace BotApi.DTO
{
    public class RateUpdateDTO
    {
        public int UserId { get; set; }
        public IEnumerable<CurrencyRateDTO> Rates { get; set; }
    }
}
