﻿using AutoMapper;
using BotApi.Aggregators.Interfaces;
using BotApi.Data.DAL;
using BotApi.Data.Models;
using BotApi.DTO;
using BotApi.Services.Interfaces;
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

        public async Task<IEnumerable<RateUpdateDTO>> Aggregate()
        {
            var markets = await _uow.GetRepository<Market>().GetAllAsync();
            foreach (var market in markets) await AggregateMarket(market);

            /** 
             * Form responses
             * TODO: REWRITE THIS OMG
            **/
            var users = await _uow.GetRepository<User>().GetAllAsync();
            return users.Select(u => new RateUpdateDTO() {
                UserId = u.ChatId,
                Rates = u.Subscriptions.Select(s => new CurrencyRateDTO() {
                    Currency = _mapper.Map<Currency, CurrencyDTO>(s.Currency.Currency),
                    Market = _mapper.Map<Market, MarketDTO>(s.Currency.Market),
                    Rate = s.Currency.Rates.Last().Rate,
                    UpdatedAt = s.Currency.Rates.Last().Date
                })
            });
        }

        private async Task AggregateMarket(Market market)
        {
            
            var driver = _driverFactory.BuildDriver(market.AggregationDriver);
            var rateUpdates = await driver.Aggreagate(market.Currencies);

            var repo = _uow.GetRepository<CurrencyRate>();
            foreach (var entity in rateUpdates) {
                await repo.CreateAsync(entity);
            }
            await _uow.SaveAsync();
        }
    }
}
