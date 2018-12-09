using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotApi.Data;
using BotApi.Data.Models;
using BotApi.Responses;
using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service; 
        public SubscriptionController(ISubscriptionService service)
        {
            _service = service;
        }

        // GET api/subscription
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetAvailable()
        {
            return Ok(new AvailableSubscriptionResponse()
            {
                Success = true,
                Currencies = await _service.GetAvailableCurrencies()
            });
        }

        // GET api/subscription/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Subscription>>> Get(int id)
        {
            return Ok(await _service.GetSubscriptionsByUser(id));
        }

        // POST api/subscription
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/subscription/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/subscription/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
