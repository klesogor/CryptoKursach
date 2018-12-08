using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Bot
{
    public class Reply : IReply
    {
        public string Message { get; set ; }
        public IReplyMarkup Markup { get; set; }
    }
}
