using System.Collections.Generic;
using System.Threading.Tasks;
using Bot.APIs;
using Bot.Entities;

namespace Bot.Services
{
    public class AggregationService : AbstractRateService
    {
        public AggregationService(IAPI api) : base(api)
        {
        }

        public async Task<List<RateUpdate>> AggregateUpdates()
        {
            return await _api.AggregateUpdates();
        }
    }
}
