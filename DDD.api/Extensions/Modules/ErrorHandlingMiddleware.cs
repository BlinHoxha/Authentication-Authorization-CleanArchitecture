using DDD.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace DDD.api.Extensions.Modules;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    { 
        _next = next;
    }

    public async Task Invoke (HttpContext context)
    {
        try
        {

        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        var stackTrace = string.Empty;
        var message = "";

        var exceptionType = ex.GetType();

        if(exceptionType == typeof(NotFoundException))
        {
            message = ex.Message;
            status = HttpStatusCode.BadRequest;
            stackTrace = ex.StackTrace;
        }
        else if(exceptionType == typeof(NotImplementedException))
        { 
            message = ex.Message;
            status = HttpStatusCode.NotImplemented;
            stackTrace = ex.StackTrace;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotImplemented;
            stackTrace = ex.StackTrace;
        }
        else
        {
            message = ex.Message;
            status = HttpStatusCode.InternalServerError;
            stackTrace = ex.StackTrace;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, stackTrace });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) status;

        return context.Response.WriteAsync(exceptionResult);
    }
}
