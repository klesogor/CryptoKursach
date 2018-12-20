using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using Bot.Bot;
using Bot.Bot.Replies.Interfaces;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace Bot.Routers
{
    public class Route
    {
        public string Name { get; set; }

        public string RawRoute { get; set; }

        public string CompiledRoute { get; set; }

        public Func<ParameterBag, Chat, Task<IReply>> Handler { get; set; }

        public string SetParam(string param, object value)
        {
            return Regex.Replace(RawRoute, "{" + param + "}", value.ToString());
        }

        public string SetParam(Dictionary<string, string> param)
        {
            var temp = RawRoute;
            foreach (var kval in param)
            {
                temp = Regex.Replace(RawRoute, "{" + kval.Key + "}", kval.Value.ToString());
            }

            return temp;
        }
    }
}
