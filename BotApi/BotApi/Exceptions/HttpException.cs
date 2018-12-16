using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Exceptions
{
    public class HttpException: Exception
    {
        public int Status { get; set; } = 400;

        public HttpException(string message): base(message){}
    }
}
