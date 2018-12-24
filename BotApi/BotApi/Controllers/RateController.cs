using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BotApi.Controllers
{
    public class RateController: ControllerBase
    {
        private readonly IRateService _rateService;

        public RateController(IRateService rateService)
        {
            _rateService = rateService;
        }

        [HttpGet]
        [Route("/api/v1/currency/rate/{currencyId}/{marketId}")]
        public async Task<IActionResult> GetRate(int currencyId, int marketId)
        {
            return Ok(await _rateService.GetRate(currencyId, marketId));
        }
    }
}