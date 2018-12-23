using Bot.Bot.Replies.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Bot
{
    public interface IBot
    {
        void Start();

        void SendMessage(int chatId, IReply reply);
    }
}
