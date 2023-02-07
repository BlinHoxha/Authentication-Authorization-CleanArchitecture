namespace DDD.Application.Exceptions
{
    public class UserNotAddedToRoleException : Exception
    {
        public UserNotAddedToRoleException(string message) : base(message)
        {
        }
    }
}
