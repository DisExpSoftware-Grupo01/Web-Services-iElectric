using System.Net;
using System.Text.Json;
using web_services_ielectric.Shared.Exceptions;

namespace web_services_ielectric.Security.Authorization.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case AppException e:
                    // Custom Application Error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // Custom Application Error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // Custom Application Error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result); 
        }
    }
}