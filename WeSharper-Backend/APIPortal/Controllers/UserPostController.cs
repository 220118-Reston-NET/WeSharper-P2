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
        //GET: api/UserPost/Feeds
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.Feeds)]
        public async Task<IActionResult> GetFeeds()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                var result = _userPBL.GetFeedsByUserID(p_userID);
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
        //GET: api/UserPost/UserPosts
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.UserPosts)]
        public async Task<IActionResult> GetUserPosts()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                var result = _userPBL.GetUserPostsByUserID(p_userID);
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

        //GET: api/UserPost/UserPosts/5
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.UserPost)]
        public IActionResult GetUserPost(string p_postID)
        {
            try
            {
                Post userPost = _userPBL.GetUserPost(p_postID);
                Log.Information("Route: " + RouteConfigs.UserPost);
                Log.Information("Get user post!");

                return Ok(userPost);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPost);
                Log.Warning(e.Message);

                return NotFound(e.Message);
            }
        }

        //POST: api/UserPost/UserPosts
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.UserPosts)]
        public async Task<IActionResult> PostUserPost([FromBody] UserPost p_post)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                Post _newPost = new Post()
                {
                    UserId = p_userID,
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

        //PUT: api/UserPost/UserPosts/5
        [Authorize(Roles = "User")]
        [HttpPut(RouteConfigs.UserPost)]
        public async Task<IActionResult> UpdateUserPost(string p_postID, [FromBody] UserPost p_post)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                Post _updatedPost = new Post()
                {
                    PostId = p_postID,
                    UserId = p_userID,
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

        //DELETE: api/UserPost/UserPosts/5
        [Authorize(Roles = "User")]
        [HttpDelete(RouteConfigs.UserPost)]
        public async Task<IActionResult> DeleteUserPost(string p_postID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                Post _deletedPost = new Post()
                {
                    PostId = p_postID,
                    UserId = p_userID
                };
                var result = _userPBL.DeleteUserPost(_deletedPost);
                Log.Information("Route: " + RouteConfigs.UserPost);
                Log.Information("Deleted user post!");

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
        //POST: api/UserPost/UserPosts/5/Comments
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.UserPostComments)]
        public async Task<IActionResult> PostUserPostComment(string p_postID, string p_comment)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                PostComment _newPostComment = new PostComment()
                {
                    PostId = p_postID,
                    UserId = p_userID,
                    PostComment1 = p_comment
                };
                var result = _userPBL.AddNewUserPostComment(_newPostComment);
                Log.Information("Route: " + RouteConfigs.UserPostComments);
                Log.Information("Posted new user post comment!");

                return Created("Posted new comment!", result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPostComments);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        //PUT: api/UserPost/UserPosts/5/Comments/5
        [Authorize(Roles = "User")]
        [HttpPut(RouteConfigs.UserPostComment)]
        public async Task<IActionResult> UpdateUserPostComment(string p_postID, string p_postCommentID, string p_comment)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                PostComment _updatePostComment = new PostComment()
                {
                    PostId = p_postID,
                    UserId = p_userID,
                    CommentId = p_postCommentID,
                    PostComment1 = p_comment
                };
                var result = _userPBL.UpdateUserPostComment(_updatePostComment);
                Log.Information("Route: " + RouteConfigs.UserPostComment);
                Log.Information("Updated user post comment!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPostComment);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        //DELETE: api/UserPost/UserPosts/5/Comments/5
        [Authorize(Roles = "User")]
        [HttpDelete(RouteConfigs.UserPostComment)]
        public async Task<IActionResult> DeleteUserPostComment(string p_postID, string p_postCommentID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                PostComment _deletedPostComment = new PostComment()
                {
                    PostId = p_postID,
                    UserId = p_userID,
                    CommentId = p_postCommentID
                };
                var result = _userPBL.DeleteUserPostComment(_deletedPostComment);
                Log.Information("Route: " + RouteConfigs.UserPostComment);
                Log.Information("Deleted user post comment!");

                return Ok("Deleted user post comment!");
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPostComment);
                Log.Warning(e.Message);

                return Conflict(e.Message);
            }
        }

        /*
            USER POST REACTION APIs
        */
        //POST: api/UserPost/UserPosts/5/Reactions
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.UserPostReactions)]
        public async Task<IActionResult> ReactUserPost(string p_postID, string p_reactID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                PostReact _postReaction = new PostReact()
                {
                    PostId = p_postID,
                    UserId = p_userID,
                    ReactId = p_reactID
                };
                var result = _userPBL.ReactUserPost(_postReaction);
                Log.Information("Route: " + RouteConfigs.UserPostReactions);
                Log.Information("Reacted user post!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPostReactions);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        /*
            USER POST COMMENT REACTION APIs
        */
        //POST: api/UserPost/UserPosts/5/Comments/5/Reactions
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.UserPostCommentReactions)]
        public async Task<IActionResult> ReactUserPostCommment(string p_postID, string p_postCommentID, string p_reactID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                PostCommentReact _postCommentReaction = new PostCommentReact()
                {
                    CommentId = p_postCommentID,
                    UserId = p_userID,
                    ReactId = p_reactID
                };
                var result = _userPBL.ReactUserPostComment(_postCommentReaction);
                Log.Information("Route: " + RouteConfigs.UserPostCommentReactions);
                Log.Information("Reacted user post comment!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.UserPostCommentReactions);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }
    }
}
