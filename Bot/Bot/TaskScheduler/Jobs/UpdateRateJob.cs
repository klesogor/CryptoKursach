using Bot.Bot;
using Bot.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.TaskScheduler.Jobs
{
    public class UpdateRateJob : IJob
    {
        private readonly IBot _bot;
        private readonly AggregationService _aggregationService;

        public UpdateRateJob(IBot bot, AggregationService aggregationService)
        {
            _bot = bot;
            _aggregationService = aggregationService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var updates = await _aggregationService.AggregateUpdates();
            foreach (var update in updates)
            {
                _bot.SendMessage(update.UserId, _aggregationService.RenderRate(update.Rates.ToArray()));
            }
        }
    }
}
