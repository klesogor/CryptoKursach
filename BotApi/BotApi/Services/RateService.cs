using AutoMapper;
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
    public class RateService: IRateService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RateService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CurrencyRateDTO> GetRate(int currencyId, int marketId)
        {
            var currencyMarket = (await _uow.GetRepository<CurrencyMarket>()
                .GetAllAsync(cm => cm.MarketId == marketId && cm.CurrencyId == currencyId)).First();
            var lastRate = currencyMarket.Rates.OrderByDescending(r => r.Date).First();
            return new CurrencyRateDTO
            {
                Currency = _mapper.Map<Currency,CurrencyDTO>(currencyMarket.Currency),
                Market = _mapper.Map<Market,MarketDTO>(currencyMarket.Market),
                Rate = lastRate.Rate,
                UpdatedAt = lastRate.Date
            };
        }
    }
}
