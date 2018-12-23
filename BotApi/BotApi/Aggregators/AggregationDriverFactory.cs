using BotApi.Aggregators.Drivers;
using BotApi.Aggregators.Interfaces;
using BotApi.Exceptions;
using Microsoft.Extensions.Configuration;

namespace BotApi.Aggregators
{
    public class AggregationDriverFactory: IDriverFactory
    {
        private readonly IConfiguration _config;

        public AggregationDriverFactory(IConfiguration config)
        {
            _config = config;
        }

        public IAggregationDriver BuildDriver(AggregationDriverType type)
        {
            switch (type)
            {
                case AggregationDriverType.CoinMarketCapDriver:
                    return new CointMarketCapDriver(_config);
                default:
                    throw new HttpException("This aggregation driver is not implemented yet") { Status = 404 };
            }
        }
    }
}
