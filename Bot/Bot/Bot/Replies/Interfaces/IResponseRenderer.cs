
namespace Bot.Bot.Replies.Interfaces
{
    public interface IResponseRenderer
    {
        void RenderMenu(IMenuReply reply, int chatId);

        void RenderPlainText(ITextReply reply, int chatId);

        void RenderImage(IImageReply reply, int chatId);
    }
}
