using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Exceptions
{
    public class UnregisteredUserException : HttpException
    {
        private const string _message = "You are unregistered. Please, use /start command to register";
        public UnregisteredUserException() : base(_message)
        {
            Status = 401;
        }
    }
}
