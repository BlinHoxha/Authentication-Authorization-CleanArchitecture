namespace DDD.Application.Exceptions
{
    public class InvalidEmailOrPasswordException : Exception
    {
        public InvalidEmailOrPasswordException(string message) : base(message)
        {
        }
    }
}
