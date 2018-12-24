using BotApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Controllers
{
    public class AggregationController: ControllerBase
    {
        private readonly IAggregationService _aggregationService;

        public AggregationController(IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        [HttpPost]
        [Route("/api/v1/aggregate")]
        public async Task<IActionResult> Aggregate()
        {
            return Ok(await _aggregationService.Aggregate());
        }
    }
}
