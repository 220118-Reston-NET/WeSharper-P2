using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;
using WeSharper.APIPortal.AuthenticationService.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace WeSharper.APIPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendManagementBL _friendBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        public FriendController(IFriendManagementBL friendBL,
                                IHttpContextAccessor httpContextAccessor,
                                UserManager<ApplicationUser> userManager)
        {
            _friendBL = friendBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
        }

        // GET: api/Friend/Friends
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.Friends)]
        public async Task<IActionResult> GetAllFriendByUserID()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting All Friends of UserID: " + p_userID);
                return Ok(_friendBL.GetAllFriendByUserID(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Friends + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // GET: api/Friend/IncomingFriends
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.IncomingFriends)]
        public async Task<IActionResult> GetAllIncomingFriendByUserID()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting all Incoming Request Friends of UserID: " + p_userID);
                return Ok(_friendBL.GetAllIncomingFriendByUserID(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.IncomingFriends + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // GET: api/Friend/OutcomingFriends
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.OutcomingFriends)]
        public async Task<IActionResult> GetAllOutcomingFriendByUserID()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting all Outcoming Request Friends of UserID: " + p_userID);
                return Ok(_friendBL.GetAllOutcomingFriendByUserID(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.OutcomingFriends + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // GET: api/Friend/RecommendFriends
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.RecommendFriends)]
        public async Task<IActionResult> GetAllRecommendFriendsByUserID()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting all Recommended Friends of UserID: " + p_userID);
                return Ok(_friendBL.GetAllRecommenedFriendByUserID(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.RecommendFriends + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // GET: api/Friend/Friends/5/Profile
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.FriendProfile)]
        public async Task<IActionResult> GetFriendProfileByFriendID(string p_friendID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting Friend Profile By ID: " + p_friendID);
                return Ok(_friendBL.GetFriendProfileByFriendID(p_friendID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.FriendProfile + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // GET: api/Friend/Friends/5/Posts
        [Authorize(Roles = "User")]
        [HttpGet(RouteConfigs.FriendPosts)]
        public async Task<IActionResult> GetFriendPostsByFriendID(string p_friendID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting Friend Profile By ID: " + p_friendID);
                return Ok(_friendBL.GetFriendPostsByFriendID(p_friendID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.FriendPosts + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // POST: api/Friend/Friends/5/Add
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.AddFriend)]
        public async Task<IActionResult> AddFriend(string p_friendID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Add Friend By FriendID: " + p_friendID);
                return Ok(_friendBL.AddFriend(p_userID, p_friendID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.AddFriend + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // POST: api/Friend/Friends/5/Remove
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.RemoveFriend)]
        public async Task<IActionResult> RemoveFriend(string p_friendID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Remove Friend By FriendID: " + p_friendID);
                return Ok(_friendBL.RemoveFriend(p_userID, p_friendID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.RemoveFriend + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // POST: api/Friend/Friends/5/Add
        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.AcceptFriend)]
        public async Task<IActionResult> AcceptFriend(string p_friendID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Accept Friend By FriendID: " + p_friendID);
                return Ok(_friendBL.AcceptFriend(p_userID, p_friendID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.AcceptFriend + ": " + e.Message);
                return Conflict(e.Message);
            }
        }
    }
}
