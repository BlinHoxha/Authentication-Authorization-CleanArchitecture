namespace DDD.Application.Exceptions
{
    public class RoleNotExistException : Exception
    {
        public RoleNotExistException(string message) : base(message)
        {
        }
    }
}
