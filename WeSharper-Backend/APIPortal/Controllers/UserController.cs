
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
using WeSharper.APIPortal.DataTransferObject;
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
                return Ok(_profileBL.GetAllProfiles() );
            }
            catch(System.Exception exe)
            {
                return Conflict(exe.Message);
            }
        }

        // Put: api/Profile
        [HttpGet(RouteConfigs.GetAProfile)]
        public IActionResult GetAProfile([FromQuery] string userId)
        {
            try
            {    
                return Ok( _profileBL.GetAProfile(userId) );
            }
            catch(System.Exception exe)
            {
                return NotFound(exe.Message);
            }
        }

        // Put: api/Profile
        [HttpPut(RouteConfigs.UpdateProfile)]
        public IActionResult UpdateProfile([FromQuery] Profile ProfileForm)
        {
            try{
                _profileBL.UpdateProfile(ProfileForm);
                return Ok("Profile Updated");
            }
            catch(System.Exception exe)
            {
                return NotFound(exe.Message);
            }
        }
    }
}
