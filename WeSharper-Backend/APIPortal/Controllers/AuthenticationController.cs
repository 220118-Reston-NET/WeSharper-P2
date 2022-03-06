using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.AuthenticationService.Interfaces;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObject;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.APIPortal.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccessTokenManager _accessTokenManager;
        private readonly IProfileManagementBL _profileBL;
        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IAccessTokenManager accessTokenManager,
                                        IProfileManagementBL profileBL)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accessTokenManager = accessTokenManager;
            _profileBL = profileBL;
        }

        // POST: api/Authentication/Register
        [HttpPost(RouteConfigs.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterForm registerFrom)
        {

            // //Validate the phonenumber
            // if (string.IsNullOrEmpty(registerFrom.PhoneNumber))
            // {
            //     return BadRequest(new { Result = "Phone number cannot be null or empty" });
            // }
            // if (!registerFrom.PhoneNumber.All(Char.IsDigit))
            // {
            //     return BadRequest(new { Result = "Phone number cannot contains any letter" });
            // }
            // if (registerFrom.PhoneNumber.Length < 10)
            // {
            //     return BadRequest(new { Result = "Phone number length should be greater or equal to 10" });
            // }

            if (!(await _roleManager.RoleExistsAsync("User")))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            var newId = Guid.NewGuid().ToString();
            ApplicationUser _identity = new ApplicationUser()
            {
                Id = newId,
                UserName = registerFrom.Username,
                Email = registerFrom.Email,
                PhoneNumber = registerFrom.PhoneNumber,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(_identity, registerFrom.Password);

            if (result.Succeeded)
            {
                var userFromDB = await _userManager.FindByNameAsync(_identity.UserName);

                _profileBL.AddNewProfile(new Profile()
                {
                    ProfileId = Guid.NewGuid().ToString(),
                    UserId = userFromDB.Id
                });

                // Add default role to user("User")
                await _userManager.AddToRoleAsync(userFromDB, "User");

                Log.Warning("Route: " + RouteConfigs.Register);
                Log.Information("Register Sucees " + _identity.UserName);
                return Ok(new { Result = "Register Success!" });
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                }

                Log.Warning("Route: " + RouteConfigs.Register);
                Log.Warning($"Register Fail: {stringBuilder.ToString()}");
                return BadRequest(new { Result = $"Register Fail: {stringBuilder.ToString()}" });
            }
        }

        // POST: api/Authentication/Login
        [HttpPost(RouteConfigs.Login)]
        public async Task<IActionResult> Login([FromBody] LoginForm loginForm)
        {
            var userFromDB = await _userManager.FindByNameAsync(loginForm.Username);

            if (userFromDB == null)
            {
                Log.Warning("Route: " + RouteConfigs.Login);
                Log.Warning("Login Failed! User didn't exist in the database!");
                return BadRequest("Login Failed! User didn't exist in the database!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDB, loginForm.Password, false);

            if (!result.Succeeded)
            {
                Log.Warning("Route: " + RouteConfigs.Login);
                Log.Warning("Login Failed! Password didn't matched in the database!");
                return BadRequest("Login Failed! Password didn't matched in the database!");
            }
            var roles = await _userManager.GetRolesAsync(userFromDB);

            Log.Information("Route: " + RouteConfigs.Login);
            Log.Information("Login Succesfully!");
            return Ok(new
            {
                Result = result,
                Username = userFromDB.UserName,
                Email = userFromDB.Email,
                Token = _accessTokenManager.GenerateToken(userFromDB, roles)
            });
        }
    }
}
