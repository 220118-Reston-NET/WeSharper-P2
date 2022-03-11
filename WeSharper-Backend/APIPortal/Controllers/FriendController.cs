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

        // GET: api/Friend/AllFriends
        [HttpGet(RouteConfigs.AllFriends)]
        public async Task<IActionResult> GetAllFriends()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                List<Profile> _listOfProfiles = new List<Profile>();
                List<FriendProfile> _listFriendProfiles = new List<FriendProfile>();
                _listOfProfiles = _friendBL.GetAllFriendProfiles();
                foreach (var item in _listOfProfiles)
                {
                    _listFriendProfiles.Add(new FriendProfile()
                    {
                        FriendProfileInformation = _friendBL.GetFriendProfileByFriendID(item.UserId),
                        Relationship = _friendBL.GetRelationship(p_userID, item.UserId)
                    });
                };

                Log.Information("Getting All Friends");
                return Ok(_listFriendProfiles);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.AllFriends + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // GET: api/Friend/Friends/5
        [HttpGet(RouteConfigs.Friend)]
        public async Task<IActionResult> GetFriendProfileByFriendID(string p_friendID)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                FriendProfile _friendProfile = new FriendProfile()
                {
                    FriendProfileInformation = _friendBL.GetFriendProfileByFriendID(p_friendID),
                    Relationship = _friendBL.GetRelationship(p_userID, p_friendID)
                };

                Log.Information("Getting Friend Profile By ID: " + p_friendID);
                return Ok(_friendProfile);
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Friend + ": " + e.Message);
                return Conflict(e.Message);
            }
        }

        // POST: api/Friend/Friends/5/add
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
    }
}
