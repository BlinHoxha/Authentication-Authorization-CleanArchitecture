using DDD.Application.InterfaceServices;

namespace DDD.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;
}
