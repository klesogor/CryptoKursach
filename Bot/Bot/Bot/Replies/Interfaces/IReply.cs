namespace Bot.Bot.Replies.Interfaces
{
    public interface IReply
    {
        void Render(IResponseRenderer renderer, int chatId);
    }
}
