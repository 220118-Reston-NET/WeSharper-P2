using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WeSharper.APIPortal.AuthenticationService.Middlewares;
using WeSharper.APIPortal.AuthenticationService.Interfaces;

namespace WeSharper.APIPortal.AuthenticationService.Middlewares
{
    public class AccessTokenManagerMiddleware : IAccessTokenManagerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IAccessTokenManager accessTokenManager;

        public AccessTokenManagerMiddleware(RequestDelegate next, IAccessTokenManager accessTokenManager)
        {
            this.next = next;
            this.accessTokenManager = accessTokenManager;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (await accessTokenManager.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}