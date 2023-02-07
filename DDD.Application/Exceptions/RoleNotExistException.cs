namespace DDD.Application.Exceptions
{
    public class RoleAlreadyExistException : Exception
    {
        public RoleAlreadyExistException(string message) : base(message)
        {
        }
    }
}
