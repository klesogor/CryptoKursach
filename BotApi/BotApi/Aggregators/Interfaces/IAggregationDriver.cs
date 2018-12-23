using BotApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Aggregators.Interfaces
{
    public interface IAggregationDriver
    {
        Task<List<CurrencyRate>> Aggreagate(IEnumerable<CurrencyMarket> CurrencyIds);
    }
}
