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

        
        // POST: api/Group
        [HttpPost(RouteConfigs.Groups)]
        public IActionResult AddNewGroup([FromQuery] GroupForm g_group, string userId)
        {
            try
            {
                WeSharper.Models.Group newGroup = new WeSharper.Models.Group(){
                    GroupManagerId = userId,
                    GroupName = g_group.GroupName,
                    GroupPicture = g_group.GroupPicture,
                    Description = g_group.Description,
                };
                newGroup = _groupBL.AddNewGroup(newGroup);
                Log.Information("Group Successfully created");
                return Created("Has created ", newGroup);
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }

        // Get: api/Group
        [HttpGet(RouteConfigs.Groups)]
        public IActionResult GetAllGroups()
        {  
            try{
                Log.Information("Getting All Groups");
                return Ok(_groupBL.GetAllGroups() );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Groups + ": " +  exe.Message);
                return Conflict(exe.Message);
            }
        }

        // PUT: api/Group
        [HttpPut(RouteConfigs.Groups)]
        public IActionResult UpdateGroup([FromBody] GroupForm g_group, string userId, string? newManager)
        {
            try
            {
                WeSharper.Models.Group updatedGroup = new WeSharper.Models.Group(){
                    GroupId = g_group.GroupId,
                    GroupManagerId = newManager,
                    GroupName = g_group.GroupName,
                    GroupPicture = g_group.GroupPicture,
                    Description = g_group.Description,
                };
                _groupBL.UpdateGroupInformation(updatedGroup, userId);
                Log.Information("Group Successfully updated");
                return Ok("Group Updated");
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
                return BadRequest(exe.Message);
            }
        }

        // DELETE: api/Group/5
        [HttpDelete(RouteConfigs.Groups)]
        public IActionResult DeleteGroup(Guid GroupID, Guid userId)
        {
            try
            {
                _groupBL.DeleteGroup(GroupID.ToString(), userId.ToString());
                Log.Information("Group Successfully deleted");
                return Ok("Group Disactivated");
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Groups + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }
    } 
}
