using AutoMapper;
using BotApi.Aggregators.Interfaces;
using BotApi.Data.DAL;
using BotApi.Data.Models;
using BotApi.DTO;
using BotApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDriverFactory _driverFactory;
        private readonly IMapper _mapper;

        public AggregationService(IUnitOfWork uow, IDriverFactory driverFactory, IMapper mapper)
        {
            _uow = uow;
            _driverFactory = driverFactory;
            _mapper = mapper;
        }

        public async Task<List<RateUpdateDTO>> Aggregate()
        {
            var markets = await _uow.GetRepository<Market>().GetAllAsync();
            foreach (var market in markets) AggregateMarket(market);

            //form responses
            var subscriptions = await _uow.GetRepository<Subscription>().GetAllAsync();
            
        }

        private async void AggregateMarket(Market market)
        {
            var driver = _driverFactory.BuildDriver(market.AggregationDriver);
            var rateUpdates = await driver.Aggreagate(market.Currencies);
            var repo = _uow.GetRepository<CurrencyRate>();
            foreach (var entity in rateUpdates) {
                await repo.CreateAsync(entity);
            }
        }
    }
}
