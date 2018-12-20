using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BotApi.Data.Models;
using BotApi.DTO;
using BotApi.Requests;
using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    public class SubscriptionController : TelegramBotController
    {
        private readonly ISubscriptionService _service;
        public SubscriptionController(
            ISubscriptionService service, 
            IUserService userService, 
            IMapper mapper
            ): base(mapper, userService)
        {
            _service = service;
        }

        
        [HttpGet]
        [Route("/api/v1/currencies")]
        public async Task<IActionResult> GetAvailableCurrencies()
        {
            var result = await _service.GetAvailableCurrencies();
            var mapped = _mapper.Map<
                    IEnumerable<Currency>,
                    IEnumerable<CurrencyDTO>
                    >(result);
            return Ok(mapped);   
        }

        [HttpGet]
        [Route("/api/v1/subscription")]
        public async Task<IActionResult> GetSubscriptions()
        {
            return Ok(
                       (await _service.GetSubscriptionsByUser(this.GetCurrentChatId()))
                       .Select(s => new SubscriptionDTO()
                       {
                           SubscriptionId = s.Id,
                           Currency = s.Currency.Currency,
                           Market = s.Currency.Market
                       }
                       )  
                );
        }

        [HttpGet]
        [Route("/api/v1/currency/{id}/markets")]
        public async Task<IActionResult> GetMarkets(int id)
        {
            return Ok(
                _mapper.Map<
                    IEnumerable<Market>,
                    IEnumerable<MarketDTO>
                    >(await _service.GetMarketsByCurrency(id))
                );
        }

        [HttpPost]
        [Route("/api/v1/subscribe")]
        public async Task<IActionResult> AddSubscription([FromBody] SubscriptionRequest request)
        {
            return Ok(await _service.Subscribe(request.CurrencyId, request.MarketId, this.GetCurrentChatId()));
        }

        [HttpDelete]    
        [Route("/api/v1/subscription/{id}")]
        public async Task<IActionResult> RemoveSubscription(int id)
        {
            return Ok(await _service.Unsibscribe(id));
        }
    }
}
