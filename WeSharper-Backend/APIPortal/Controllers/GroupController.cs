using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;
using WeSharper.APIPortal.AuthenticationService.Interfaces;

namespace APIPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {

        private readonly IGroupManagementBL _groupBL;
        public GroupController(IGroupManagementBL g_groupBL)
        {
            _groupBL = g_groupBL;
        }


        // // POST: api/Group
        // [HttpPost(RouteConfigs.Groups)]
        // public IActionResult AddNewGroup([FromQuery] GroupForm g_group, string userId)
        // {
        //     try
        //     {
        //         Group newGroup = new Group()
        //         {
        //             GroupManagerId = userId,
        //             GroupName = g_group.GroupName,
        //             GroupPicture = g_group.GroupPicture,
        //             Description = g_group.Description,
        //         };
        //         newGroup = _groupBL.AddNewGroup(newGroup);
        //         Log.Information("Group Successfully created");
        //         return Created("Has created ", newGroup);
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
        //         return BadRequest(exe.Message);
        //     }
        // }

        // // Get: api/Group
        // [HttpGet(RouteConfigs.Groups)]
        // public IActionResult GetAllGroups()
        // {
        //     try
        //     {
        //         Log.Information("Getting All Groups");
        //         return Ok(_groupBL.GetAllGroups());
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
        //         return Conflict(exe.Message);
        //     }
        // }

        // // PUT: api/Group
        // [HttpPut(RouteConfigs.Groups)]
        // public IActionResult UpdateGroup([FromBody] GroupForm g_group, string userId, string? newManagerId)
        // {
        //     try
        //     {
        //         Group updatedGroup = new Group()
        //         {
        //             GroupId = g_group.GroupId,
        //             GroupManagerId = newManagerId,
        //             GroupName = g_group.GroupName,
        //             GroupPicture = g_group.GroupPicture,
        //             Description = g_group.Description,
        //         };
        //         _groupBL.UpdateGroupInformation(updatedGroup, userId);
        //         Log.Information("Group Successfully updated");
        //         return Ok("Group Updated");
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
        //         return BadRequest(exe.Message);
        //     }
        // }

        // // DELETE: api/Group/5
        // [HttpDelete(RouteConfigs.Groups)]
        // public IActionResult DeleteGroup(Guid GroupID, Guid userId)
        // {
        //     try
        //     {
        //         _groupBL.DeleteGroup(GroupID.ToString(), userId.ToString());
        //         Log.Information("Group Successfully deleted");
        //         return Ok("Group Disactivated");
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
        //         return Conflict(exe.Message);
        //     }
        // }

        // [HttpGet("Testing")]
        // public IActionResult GetAllGroupUsers()
        // {
        //     try
        //     {
        //         Log.Information("Getting all group users");
        //         return Ok(_groupBL.GetAllGroupUsers());
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + " GetAllGroups " + ": " + exe.Message);
        //         return Conflict(exe.Message);
        //     }
        // }

        // [HttpGet(RouteConfigs.ApprovedGroupUser)]
        // public IActionResult GetAprovedUsersInGroup(string groupId)
        // {
        //     try
        //     {
        //         Log.Information("Getting Join Requests for a group");
        //         return Ok(_groupBL.GetApprovedUsersInGroup(groupId));
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + "Test 2" + ": " + exe.Message);
        //         return Conflict(exe.Message);
        //     }
        // }

        // [HttpGet(RouteConfigs.GroupUsers)]
        // public IActionResult GetGroupJoinRequests([FromQuery] string groupId)
        // {
        //     try
        //     {
        //         Log.Information("Getting Join Requests for a group");
        //         return Ok(_groupBL.GetGroupUnapprovedJoinRequests(groupId));
        //     }
        //     catch (System.Exception exe)
        //     {
        //         Log.Warning("Route:" + RouteConfigs.GroupUsers + ": " + exe.Message);
        //         return Conflict(exe.Message);
        //     }
        // }
    }
}
