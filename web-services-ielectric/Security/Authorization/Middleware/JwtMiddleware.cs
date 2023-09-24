using web_services_ielectric.Security.Authorization.Handlers.Interfaces;
using web_services_ielectric.Security.Domain.Services;

namespace web_services_ielectric.Security.Authorization.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;


    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IUserService userService, IJwtHandler handler)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);

        if (userId != null)
        {
            // Attach user to context
            context.Items["User"] = await userService.GetByIdAsync(userId.Value);
        }

        await _next(context);

    }
}