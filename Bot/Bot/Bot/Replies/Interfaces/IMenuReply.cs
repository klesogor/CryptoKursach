using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Bot.Replies.Interfaces
{
    public interface IMenuReply: ITextReply
    {
        IReplyMarkup Markup { get; set; }
    }
}
