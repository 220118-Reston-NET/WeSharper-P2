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

namespace WeSharper.APIPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {

        private readonly IFriendManagementBL _friendBL;
        public FriendController(IFriendManagementBL f_friendBL)
        {
            _friendBL = f_friendBL;
        }

        // POST: api/Friend
        [HttpPost(RouteConfigs.Friends)]
        public IActionResult AddNewFriend([FromQuery] FriendForm friend)
        {
            try
            {
                ///_friendBL.ExistingFriendId(friendName);
                Friend _newFriend = new Friend()
                {
                    RelationshipId = Guid.NewGuid().ToString(),
                    RequestedUserId = friend.RequestedUserId,
                    AcceptedUserId = friend.AcceptedUserId,
                    IsAccepted = false,
                    Relationship = friend.Relationship,
                    CreatedAt = null
                };
                _friendBL.SendFriendRequest(_newFriend);
                Log.Information("Friend request successfully sent");
                return Created("Friend request successfully sent", _newFriend);
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Friends + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }
        /* for admin
        // Get: api/Friend
        [HttpGet(RouteConfigs.Friends)]
        public IActionResult GetAllFriends()
        {  
            try{
                Log.Information("Getting All Friends");
                return Ok(_friendBL.GetAllFriends() );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Friends + ": " +  exe.Message);
                return Conflict(exe.Message);
            }
        }
        */
        // Get: api/Friend
        [HttpGet(RouteConfigs.Friend)]
        public IActionResult GetUserFriends(string f_userID)
        {
            try{
                Log.Information("Getting All Friends");
                return Ok(_friendBL.GetUserFriends(f_userID) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Friend + ": " +  exe.Message);
                return Conflict(exe.Message);
            }
        }

        // PUT: api/Friend
        [HttpPut(RouteConfigs.Friends)]
        public IActionResult UpdateFriend([FromQuery] FriendForm f_friend)
        {
            try
            {
                ///_friendBL.ExistingFriendId(friendName);
                Friend _newFriend = new Friend()
                {
                    RelationshipId = null,
                    RequestedUserId = f_friend.RequestedUserId,
                    AcceptedUserId = f_friend.AcceptedUserId,
                    IsAccepted = f_friend.IsAccepted,
                    Relationship = f_friend.Relationship,
                    CreatedAt = null
                };
                _friendBL.UpdateFriendRequest(_newFriend);
                Log.Information("Friend Successfully updated");
                return Ok("Friend Updated");
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Friends + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }

        // DELETE: api/Friend/5
        [HttpDelete(RouteConfigs.Friends)]
        public IActionResult DeleteFriend(Guid userId, Guid friendId)
        {
            try
            {
                _friendBL.DeleteFriend(userId.ToString(), friendId.ToString());
                Log.Information("Friend Successfully deleted");
                return Ok("Friend Deleted");
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Friends + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }

        [HttpGet(RouteConfigs.UnconfirmedSentFriends)]
        public IActionResult GetUserUnconfirmedSentFriends(Guid f_userID)
        {
            try
            {
                List<Friend> listOfUnacceptedSentFriendRequests = _friendBL.GetUnacceptedSentRequests(f_userID.ToString());
                Log.Information("Retrieved unaccepted sent friend requests");
                return Ok(listOfUnacceptedSentFriendRequests);
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.UnconfirmedSentFriends + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }

        [HttpGet(RouteConfigs.UserPendingFriendRequests)]
        public IActionResult GetUserPendingFriendRequests(Guid f_userID)
        {
            try
            {
                List<Friend> listOfUnacceptedRecievedFriendRequests= _friendBL.GetUserPendingFriendRequests(f_userID.ToString());
                Log.Information("Retrieved unaccepted friend requests");
                return Ok(listOfUnacceptedRecievedFriendRequests);
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.UnconfirmedSentFriends + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }
    }
}
