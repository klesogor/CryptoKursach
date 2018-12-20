
namespace BotApi.Exceptions
{
    public class DuplicatedException : HttpException
    {
        public DuplicatedException(string message) : base(message)
        {
            Status = 422;
        }
    }
}
