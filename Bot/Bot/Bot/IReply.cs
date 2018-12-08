using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Bot
{
    public interface IReply
    {
       string Message { get; set; }

       IReplyMarkup  Markup{ get; set; }
    }
}
