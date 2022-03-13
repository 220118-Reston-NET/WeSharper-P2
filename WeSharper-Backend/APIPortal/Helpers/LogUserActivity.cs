using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WeSharper.APIPortal.Extensions;
using WeSharper.APIPortal.Interfaces;
using WeSharper.BusinessesManagement.Interfaces;

namespace WeSharper.APIPortal.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        private readonly IProfileManagementBL _profileBL;

        public LogUserActivity(IProfileManagementBL profileBL)
        {
            _profileBL = profileBL;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            var userId = resultContext.HttpContext.User.GetUserId();
            var uow = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();
            var user = _profileBL.GetUserByUserID(userId);
            await uow.Complete();
        }
    }
}