
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.AuthenticationService.Interfaces;
using WeSharper.APIPortal.BlobService.Interfaces;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Implements;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Implements;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.APIPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProfileManagementBL _profileBL;
        private readonly IBlobService blobService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        public UserController(IProfileManagementBL p_profileBL,
                                IHttpContextAccessor httpContextAccessor,
                                UserManager<ApplicationUser> userManager,
                                IBlobService blobService)
        {
            _profileBL = p_profileBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["unique_name"].ToString();

            this.blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        }

        // GET: api/User/Profile
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.GetProfile)]
        public async Task<IActionResult> GetProfile()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting profile information");
                return Ok(await _profileBL.GetAProfile(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.GetProfile + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // PUT: api/User/Profile
        [Authorize(Roles = "User")]
        [HttpPut(RouteConfigs.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfile p_profile)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Profile _updatedProfile = new Profile()
                {
                    UserId = p_userID,
                    FirstName = p_profile.FirstName,
                    LastName = p_profile.LastName,
                    Bio = p_profile.Bio
                };
                await _profileBL.UpdateProfile(_updatedProfile);
                Log.Information("Profile successfully updated for " + p_profile.FirstName + " " + p_profile.LastName);
                return Ok(new { Results = "Profile Updated" });
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Hobby + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // POST: api/User/ProfilePicture
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.ProfilePicture), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadProfilePicture()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileURL = await blobService.UploadAsync(file.OpenReadStream(), fileName, file.ContentType);

                    Profile _updatedProfile = new Profile()
                    {
                        UserId = p_userID,
                        ProfilePictureUrl = fileURL
                    };
                    await _profileBL.UpdateProfilePicture(_updatedProfile);
                    Log.Information("Upload Profile Picture successfully!" + fileURL);
                    return Ok(new { fileURL });
                }
                else
                {
                    return BadRequest("Failed to upload profile picture! Please try again!");
                }
            }
            catch (System.Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
