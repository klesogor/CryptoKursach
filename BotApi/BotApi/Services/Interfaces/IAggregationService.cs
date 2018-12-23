using BotApi.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotApi.Services.Interfaces
{
    public interface IAggregationService
    {
        Task<List<RateUpdateDTO>> Aggregate();
    }
}
