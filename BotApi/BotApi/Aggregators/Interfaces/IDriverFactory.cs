using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Aggregators.Interfaces
{
    public interface IDriverFactory
    {
        IAggregationDriver BuildDriver(AggregationDriverType type);
    }
}
