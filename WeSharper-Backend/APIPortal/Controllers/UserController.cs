
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.AuthenticationService.Interfaces;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Implements;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Implements;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.APIPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProfileManagementBL _profileBL;
        public UserController(IProfileManagementBL p_profileBL){
            _profileBL = p_profileBL;
        }

        // Get: api/Profile
        [HttpGet(RouteConfigs.GetAllProfiles)]
        public IActionResult GetAllProfiles()
        {
            
            try{
                Log.Information("Getting All Profiles");
                return Ok(_profileBL.GetAllProfiles() );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Hobby + ": " +  exe.Message);
                return Conflict(exe.Message);
            }
        }

        // Get: api/Profile
        [HttpGet(RouteConfigs.GetAProfile)]
        public IActionResult GetAProfile([FromQuery] string userId)
        {
            try
            {
                Log.Information("Getting a profile");
                return Ok( _profileBL.GetAProfile(userId) );
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Hobby + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }

        // Put: api/Profile
        [HttpPut(RouteConfigs.UpdateProfile)]
        public IActionResult UpdateProfile([FromQuery] Profile ProfileForm)
        {
            try{
                _profileBL.UpdateProfile(ProfileForm);
                Log.Information("Profile successfully updated for " + ProfileForm.FirstName + " " + ProfileForm.LastName);
                return Ok("Profile Updated");
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Hobby + ": " + exe.Message);
                return NotFound(exe.Message);
            }
        }
    }
}
