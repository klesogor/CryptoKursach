using System.Collections.Generic;

namespace Bot.Entities
{
    public class RateUpdate
    {
        public int UserChatId { get; set; }
        public IEnumerable<CurrencyRate> Rates { get; set; }
    }
}
