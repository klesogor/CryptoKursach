using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Exceptions
{
    public class InvalidTokenException: HttpException
    {
        public static readonly string _message = "Invalid auth token";
        public static readonly int _status = 403;

        public InvalidTokenException(): base(_message)
        {
            Status = _status;
        }
    }
}
