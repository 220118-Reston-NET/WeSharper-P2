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
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.Models;

namespace APIPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPostController : ControllerBase
    {
        private readonly IGroupPostManagementBL _groupPBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;

        public GroupPostController(IGroupPostManagementBL groupPBL,
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<ApplicationUser> userManager)
        {
            _groupPBL = groupPBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
        }

        /*
            GROUP POST APIs
        */
        // GET: api/GroupPost
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.GroupPosts)]
        public IActionResult GetAllGroupPostsByGroupID(string p_groupID)
        {
            try
            {
                var _result = _groupPBL.GetGroupPostsByGroupID(p_groupID);
                Log.Information("Route: " + RouteConfigs.GroupPosts);
                Log.Information("Get All Posts For Group ID: " + p_groupID);

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPosts);
                Log.Warning(e.Message);
                return NotFound("Cannot find any post belongs in this group!");
            }
        }

        // GET: api/GroupPost/5
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.GroupPost)]
        public IActionResult GetAllGroupPostByGroupPostID(string p_groupID, string p_groupPostID)
        {
            try
            {
                var _result = _groupPBL.GetAllGroupPostByGroupPostID(p_groupPostID);
                Log.Information("Route: " + RouteConfigs.GroupPost);
                Log.Information("Get Group Post by Group Post ID: " + p_groupID);

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPost);
                Log.Warning(e.Message);
                return NotFound("Please check the groupPostID!");
            }
        }

        // POST: api/GroupPost
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.GroupPosts)]
        public async Task<IActionResult> PostNewPostToGroup(string p_groupID, [FromBody] GroupPost p_groupPost)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                p_groupPost.GroupId = p_groupID;
                p_groupPost.UserId = p_userID;
                var _result = _groupPBL.PostNewPostToGroup(p_groupPost);
                Log.Information("Route: " + RouteConfigs.GroupPosts);
                Log.Information("Posted a new post to groupID: " + p_groupID);

                return Created("Posted!", _result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPosts);
                Log.Warning(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        // PUT: api/GroupPost/5
        [Authorize(Roles = "User")]
        [HttpPut(RouteConfigs.GroupPost)]
        public async Task<IActionResult> UpdateGroupPost(string p_groupID, [FromBody] GroupPost p_groupPost)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                p_groupPost.GroupId = p_groupID;
                p_groupPost.UserId = p_userID;
                var _result = _groupPBL.UpdateGroupPost(p_groupPost);
                Log.Information("Route: " + RouteConfigs.GroupPost);
                Log.Information("Updated Group Post: " + p_groupPost);

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPost);
                Log.Warning(e.Message);
                return Conflict(e.Message);
            }
        }

        // DELETE: api/GroupPost/5
        [Authorize(Roles = "User")]
        [HttpDelete(RouteConfigs.GroupPost)]
        public async Task<IActionResult> DeleteGroupPostByGroupPostID(string p_groupID, string p_groupPostID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                GroupPost _deletedPost = new GroupPost()
                {
                    GroupPostId = p_groupPostID,
                    GroupId = p_groupID,
                    UserId = p_userID
                };

                var _result = _groupPBL.DeleteGroupPost(_deletedPost);
                Log.Information("Route: " + RouteConfigs.GroupPost);
                Log.Information("Deleted Group PostID: " + p_groupPostID);

                return Ok(_result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPost);
                Log.Warning(e.Message);
                return BadRequest(e.Message);
            }
        }

        /*
            GROUP POST COMMENT APIs
        */
        //POST: api/GroupPost/5/Posts/5/Comments
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.GroupPostComments)]
        public async Task<IActionResult> PostGroupPostComment(string p_groupID, string p_groupPostID, string p_groupPostComment)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                GroupPostComment _newPostComment = new GroupPostComment()
                {
                    GroupId = p_groupID,
                    GroupPostId = p_groupPostID,
                    UserId = p_userID,
                    GroupPostComment1 = p_groupPostComment
                };
                var result = _groupPBL.AddNewGroupPostComment(_newPostComment);
                Log.Information("Route: " + RouteConfigs.GroupPostComments);
                Log.Information("Posted new group post comment to GroupPostID: " + p_groupPostID);

                return Created("Posted new group post comment!", result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPostComments);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        //PUT: api/GroupPost/5/Posts/5/Comments/5
        [Authorize(Roles = "User")]
        [HttpPut(RouteConfigs.GroupPostComment)]
        public async Task<IActionResult> UpdateGroupPostComment(string p_groupID, string p_groupPostID, string p_groupPostCommentID, string p_groupPostComment)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                GroupPostComment _updatePostComment = new GroupPostComment()
                {
                    GroupPostCommentId = p_groupPostCommentID,
                    GroupId = p_groupID,
                    GroupPostId = p_groupPostID,
                    UserId = p_userID,
                    GroupPostComment1 = p_groupPostComment
                };
                var result = _groupPBL.UpdateGroupPostComment(_updatePostComment);
                Log.Information("Route: " + RouteConfigs.GroupPostComment);
                Log.Information("Updated group post comment!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPostComment);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        //DELETE: api/GroupPost/5/Posts/5/Comments/5
        [Authorize(Roles = "User")]
        [HttpDelete(RouteConfigs.GroupPostComment)]
        public async Task<IActionResult> DeleteUserPostComment(string p_groupID, string p_groupPostID, string p_groupPostCommentID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                GroupPostComment _deletedPostComment = new GroupPostComment()
                {
                    GroupPostCommentId = p_groupPostCommentID,
                    GroupId = p_groupID,
                    GroupPostId = p_groupPostID,
                    UserId = p_userID
                };
                var result = _groupPBL.DeleteGroupPostComment(_deletedPostComment);
                Log.Information("Route: " + RouteConfigs.GroupPostComment);
                Log.Information("Deleted group post comment!");

                return Ok("Deleted group post comment!");
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPostComment);
                Log.Warning(e.Message);

                return Conflict(e.Message);
            }
        }

        /*
            GROUP POST REACTION APIs
        */
        //POST: api/GroupPost/5/Posts/5/Reactions
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.GroupPostReactions)]
        public async Task<IActionResult> ReactGroupPost(string p_groupID, string p_groupPostID, string p_reactID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                GroupPostReact _postReaction = new GroupPostReact()
                {
                    GroupId = p_groupID,
                    GroupPostId = p_groupPostID,
                    UserId = p_userID,
                    ReactId = p_reactID
                };
                var result = _groupPBL.ReactGroupPost(_postReaction);
                Log.Information("Route: " + RouteConfigs.GroupPostReactions);
                Log.Information("Reacted group post!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPostReactions);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }

        /*
            GROUP POST COMMENT REACTION APIs
        */
        //POST: api/GroupPost/5/Posts/5/Comments/5/Reactions
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.GroupPostCommentReactions)]
        public async Task<IActionResult> ReactGroupPostCommment(string p_groupID, string p_groupPostID, string p_groupPostCommentID, string p_reactID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            var p_userID = userFromDB.Id;

            try
            {
                GroupPostCommentReact _postCommentReaction = new GroupPostCommentReact()
                {
                    GroupId = p_groupID,
                    GroupPostCommentId = p_groupPostCommentID,
                    UserId = p_userID,
                    ReactId = p_reactID
                };
                var result = _groupPBL.ReactGroupPostComment(_postCommentReaction);
                Log.Information("Route: " + RouteConfigs.GroupPostCommentReactions);
                Log.Information("Reacted group post comment!");

                return Ok(result);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.GroupPostCommentReactions);
                Log.Warning(e.Message);

                return StatusCode(500, e.Message);
            }
        }
    }
}
