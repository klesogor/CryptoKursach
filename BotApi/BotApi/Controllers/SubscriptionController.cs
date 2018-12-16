using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BotApi.Data;
using BotApi.Data.Models;
using BotApi.DTO;
using BotApi.Responses;
using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;
        private readonly IMapper _mapper;
        public SubscriptionController(ISubscriptionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        
        [HttpGet]
        [Route("/api/v1/currencies")]
        public async Task<IActionResult> GetAvailableCurrencies()
        {
            return Ok(_mapper.Map<
                    IEnumerable<Currency>,
                    IEnumerable<CurrencyDTO>
                    >(await _service.GetAvailableCurrencies()
                )
                );   
        }

        [HttpGet()]
        [Route("/subscriptions/{id}")]
        public async Task<IActionResult> GetSubscriptions(int id)
        {
            return Ok(await _service.GetSubscriptionsByUser(id));
        }

        /*[HttpPost]
        public void Post([FromBody] string value)
        {
        }


        // DELETE api/subscription/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
