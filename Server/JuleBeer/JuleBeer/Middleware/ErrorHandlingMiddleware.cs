using System.Net;

namespace JuleBeer.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (OperationCanceledException)
        {
            //empty response on task canceled 
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            if (ex is ArgumentNullException) code = HttpStatusCode.BadRequest;
            else if (ex is ArgumentException) code = HttpStatusCode.BadRequest;
            else if (ex is UnauthorizedAccessException) code = HttpStatusCode.Unauthorized;


            string message = ex?.Message;
            if (!string.IsNullOrWhiteSpace(ex?.InnerException?.Message))
            {
                message += "\n" + ex.InnerException.Message;
            }

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(message);
        }
    }
}



