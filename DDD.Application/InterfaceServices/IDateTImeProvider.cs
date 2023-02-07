namespace DDD.Application.InterfaceServices
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
