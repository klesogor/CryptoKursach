using Bot.Bot;
using Bot.Services;
using Bot.TaskScheduler.Jobs;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.TaskScheduler
{
    public class UpdateJobFactory : IJobFactory
    {
        private readonly IBot _bot;
        private readonly AggregationService _aggregationService;

        public UpdateJobFactory(IBot bot, AggregationService aggregationService)
        {
            _bot = bot;
            _aggregationService = aggregationService;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new UpdateRateJob(_bot, _aggregationService);
        }

        public void ReturnJob(IJob job)
        {
            //
        }
    }
}
