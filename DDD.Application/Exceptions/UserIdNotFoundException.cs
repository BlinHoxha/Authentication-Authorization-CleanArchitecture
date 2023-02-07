namespace DDD.Application.Exceptions
{
    public class UserIdNotFoundException : Exception
    {
        public UserIdNotFoundException(string message) : base(message)
        {
        }
    }
}
