using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Clay.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Clay.Api.Middleware
{
    public class AdminAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILockService lockService, IAuthenticationService authenticationService)
        {
            var userId = authenticationService.GetCurrentUserIdFromToken(context);

            if (userId == null)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var responseMessage = new { Message = "Unauthorized Access." };
                await JsonSerializer.SerializeAsync(context.Response.Body, responseMessage);
                return;
            }

            if(!authenticationService.isAdmin((int) userId))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var responseMessage = new { Message = "Unauthorized Access." };
                await JsonSerializer.SerializeAsync(context.Response.Body, responseMessage);
                return;
            }

            await _next(context);
            return;
        }
    }

    public static class AdminAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAdminAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdminAuthorizationMiddleware>();
        }
    }
}
