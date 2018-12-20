using Bot.Bot.Replies.Interfaces;

namespace Bot.Bot
{
    public class Reply : ITextReply
    {
        public string Text { get ; set; }

        public void Render(IResponseRenderer renderer, int chatId)
        {
            renderer.RenderPlainText(this,chatId);
        }
    }
}
