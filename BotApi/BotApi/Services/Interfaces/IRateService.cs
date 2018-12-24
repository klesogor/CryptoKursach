using BotApi.DTO;
using System.Threading.Tasks;

namespace BotApi.Services.Interfaces
{
    public interface IRateService
    {
        Task<CurrencyRateDTO> GetRate(int currencyId, int marketId);
    }
}
