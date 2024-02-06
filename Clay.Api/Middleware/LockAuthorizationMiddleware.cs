using Clay.Application.Services;
using Clay.Domain.Services;
using System.Security.Claims;
using System.Text.Json;

namespace Clay.Api.Middleware
{
    public class LockAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LockAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILockService lockService, IAuthenticationService authenticationService)
        {
            if (context.Request.RouteValues.TryGetValue("lockId", out var lockIdObj) && lockIdObj is string lockIdStr)
            {
                if (int.TryParse(lockIdStr, out var lockId))
                {
                    var userId = authenticationService.GetCurrentUserIdFromToken(context);

                    if (userId.HasValue)
                    {
                        // Check if the lock exists
                        var lockExists = await lockService.LockExists(lockId);
                        if (!lockExists)
                        {
                            context.Response.StatusCode = 404;
                            context.Response.ContentType = "application/json";
                            var responseMessage = new { Message = "Lock not found." };
                            await JsonSerializer.SerializeAsync(context.Response.Body, responseMessage);
                            return;
                        }

                        // Check if the user has access to the lock
                        if (lockService.CanUserAccessLock(userId.Value, lockId))
                        {
                            context.Items["UserId"] = userId;
                            await _next(context);
                            return;
                        }
                        else
                        {
                            context.Response.StatusCode = 401; // Unauthorized
                            context.Response.ContentType = "application/json";
                            var responseMessage = new { Message = "Unauthorized access. User does not have access to the lock." };
                            await JsonSerializer.SerializeAsync(context.Response.Body, responseMessage);
                            return;
                        }
                    }
                }
            }

            context.Response.StatusCode = 401; // Unauthorized
            context.Response.ContentType = "application/json";
            var unauthorizedMessage = new { Message = "Unauthorized access." };
            await JsonSerializer.SerializeAsync(context.Response.Body, unauthorizedMessage);

        }
    }

    public static class LockAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseLockAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LockAuthorizationMiddleware>();
        }
    }
}
