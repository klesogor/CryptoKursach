using Bot.Bot.Replies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.Bot.Replies
{
    public class MenuReply : IMenuReply
    {
        public IReplyMarkup Markup { get; set; }
        public string Text { get ; set; }

        public void Render(IResponseRenderer renderer,int chatId)
        {
            renderer.RenderMenu(this,chatId);
        }
    }
}
