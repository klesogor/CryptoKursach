using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BotApi.Requests
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public int ChatId { get; set; }
    }
}
