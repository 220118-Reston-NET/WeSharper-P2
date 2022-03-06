using System;
using Microsoft.AspNetCore.Builder;

namespace WeSharper.APIPortal.AuthenticationService.Middlewares
{
    public static class AccessTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenManagerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessTokenManagerMiddleware>();
        }
    }
}