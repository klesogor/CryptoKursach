using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyBot
{
    interface IGraphicService
    {
        ChartDTO GetGraphic(List<ChartDataDTO> listRate);
    }
}
