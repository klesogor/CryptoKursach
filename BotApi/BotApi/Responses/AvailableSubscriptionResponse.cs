using BotApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Responses
{
    public class AvailableSubscriptionResponse
    {
        public IEnumerable<Currency> Currencies { get; set; }
    }
}
