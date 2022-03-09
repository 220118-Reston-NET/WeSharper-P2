using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public UserPostController(IUserPostManagementBL userPBL)
        {
            _userPBL = userPBL;
        }

        /*
            USER POST APIs
        */
        //GET: api/UserPost
        [HttpGet(RouteConfigs.UserPosts)]
        public IActionResult GetUserPosts()
        {
            try
            {
                var result = _userPBL.GetUserPostsByUserID("7638146b-4454-4d4a-be7e-86a5acbd82f0");
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
