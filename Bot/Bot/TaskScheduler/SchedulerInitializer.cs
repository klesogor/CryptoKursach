using Bot.Bot;
using Bot.Services;
using Bot.TaskScheduler.Jobs;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Bot.TaskScheduler
{
    public class SchedulerInitializer
    {
        public async static Task<IScheduler> InitializeScheduler(AggregationService service,IBot bot)
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            var scheduler = await factory.GetScheduler();
            await scheduler.Start();
            scheduler.JobFactory = new UpdateJobFactory(bot, service);

            var details = JobBuilder.Create<UpdateRateJob>().WithIdentity("aggregation", "major").Build();
            var trigger = TriggerBuilder
                .Create()
                .WithIdentity("aggregation", "major")
                .StartNow()
                .WithSimpleSchedule(s => s.WithIntervalInMinutes(1).RepeatForever())
                .Build();
            await scheduler.ScheduleJob(details, trigger);

            return scheduler;
        }
    }
}
