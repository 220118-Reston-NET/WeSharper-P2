using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using WeSharper.Models;

namespace WeSharper.APIPortal.AuthenticationService.Interfaces
{
    public interface IAccessTokenManager
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
        Task<bool> IsCurrentActiveToken();
        Task<bool> IsActiveAsync(string token);
    }
}