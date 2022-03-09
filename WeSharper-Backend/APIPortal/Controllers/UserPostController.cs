using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.Models;

namespace APIPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPostController : ControllerBase
    {
        private readonly IUserPostManagementBL _userPBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        public UserPostController(IUserPostManagementBL userPBL,
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<ApplicationUser> userManager)
        {
            _userPBL = userPBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
        }

        /*
            FEEDS API
        */
        //GET: api/UserPost
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.Feeds)]
        public async Task<IActionResult> GetFeeds()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_cusID = userFromDB.UserName;

            try
            {
                var result = _userPBL.GetFeedsByUserID(p_cusID);
                Log.Information("Route: " + RouteConfigs.Feeds);
                Log.Information("Get all new feeds for user!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.Feeds);
                Log.Warning(e.Message);

                return NotFound(new { Results = "Cannot find any new feeds for this user!" });
            }
        }

        /*
            USER POST APIs
        */
        //GET: api/UserPost
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.UserPosts)]
        public async Task<IActionResult> GetUserPosts()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_cusID = userFromDB.Id;

            try
            {
                var result = _userPBL.GetUserPostsByUserID(p_cusID);
                Log.Information("Route: " + RouteConfigs.UserPosts);
                Log.Information("Get all user posts!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPosts);
                Log.Warning(e.Message);

                return NotFound(new { Results = "Cannot find any posts of user!" });
            }
        }

        //GET: api/UserPost/5
        [HttpGet(RouteConfigs.UserPost)]
        public IActionResult GetUserPost(string p_postID)
        {
            try
            {
                var result = _userPBL.GetUserPost(p_postID);
                Log.Information("Route: " + RouteConfigs.UserPost);
                Log.Information("Get user post!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPost);
                Log.Warning(e.Message);

                return NotFound(e.Message);
            }
        }

        //POST: api/UserPost
        [HttpPost(RouteConfigs.UserPosts)]
        public IActionResult PostUserPost([FromBody] UserPost p_post)
        {
            try
            {
                Post _newPost = new Post()
                {
                    PostId = Guid.NewGuid().ToString(),
                    UserId = p_post.UserId,
                    PostContent = p_post.PostContent,
                    PostPhoto = p_post.PostPhoto
                };
                var result = _userPBL.AddNewUserPost(_newPost);
                Log.Information("Route: " + RouteConfigs.UserPosts);
                Log.Information("Added new user post!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPost);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        //PUT: api/UserPost/5
        [HttpPut(RouteConfigs.UserPost)]
        public IActionResult UpdateUserPost(string p_postID, [FromBody] UserPost p_post)
        {
            try
            {
                Post _updatedPost = new Post()
                {
                    PostId = p_postID,
                    PostContent = p_post.PostContent,
                    PostPhoto = p_post.PostPhoto
                };
                var result = _userPBL.UpdateUserPost(_updatedPost);
                Log.Information("Route: " + RouteConfigs.UserPost);
                Log.Information("Update user post!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPost);
                Log.Warning(e.Message);

                return NotFound(e.Message);
            }
        }

        /*
            USER POST COMMENT APIs
        */
        // api/UserPost
    }
}
