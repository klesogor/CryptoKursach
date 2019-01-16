using Bot.Bot.Replies.Interfaces;

namespace Bot.Bot.Replies
{
    public class ImageReply : IImageReply
    {
        public string ImageUrl { get; set; }
        public string Caption { get; set; }

        public void Render(IResponseRenderer renderer, int chatId)
        {
            renderer.RenderImage(this, chatId);
        }
    }
}
