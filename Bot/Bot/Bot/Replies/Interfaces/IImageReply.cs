namespace Bot.Bot.Replies.Interfaces
{
    public interface IImageReply: IReply
    {
        string ImageUrl { get; set; }
        string Caption { get; set; }
    }
}
