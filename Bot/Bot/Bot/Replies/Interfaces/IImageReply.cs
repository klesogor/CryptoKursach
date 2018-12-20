using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Bot.Replies.Interfaces
{
    public interface IImageReply: ITextReply
    {
        string ImageUrl { get; set; }
    }
}
