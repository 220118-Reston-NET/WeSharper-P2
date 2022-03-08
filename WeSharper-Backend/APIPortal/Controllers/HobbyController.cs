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
    public class HobbyController : ControllerBase
    {

        private readonly IHobbyManagementBL _hobbyBL;
        public HobbyController(IHobbyManagementBL h_hobbyBL)
        {
            _hobbyBL = h_hobbyBL;
        }

        // POST: api/Hobby
        [HttpPost(RouteConfigs.Hobby)]
        public IActionResult AddNewHobby([FromBody] string hobbyName)
        {
            try
            {
                if (Regex.IsMatch(hobbyName, @"^[a-zA-Z]*$"))
                {
                    Hobby _newHobby = new Hobby()
                    {
                        HobbyId = Guid.NewGuid().ToString(),
                        HobbyName = hobbyName
                    };
                    _hobbyBL.AddNewHobby(_newHobby);
                    Log.Information("Hobby Successfully created");
                    return Created("Has created", _newHobby);
                }
                Log.Warning("Route: " + RouteConfigs.Hobby);
                return BadRequest(new { Result = "Hobby is in the incorrect format!" });
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Hobby + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }

        // PUT: api/Hobby
        [HttpPut(RouteConfigs.Hobby)]
        public IActionResult UpdateHobby([FromBody] Hobby p_hobby)
        {
            try
            {
                Hobby _hobby = new Hobby()
                {
                    HobbyId = p_hobby.HobbyId,
                    HobbyName = p_hobby.HobbyName
                };
                _hobbyBL.UpdateHobby(_hobby);
                Log.Information("Hobby Successfully updated");
                return Ok("Hobby Updated");
            }
            catch (System.Exception exe)
            {
                return Conflict(exe.Message);
            }
        }

        // DELETE: api/Hobby/5
        [HttpDelete(RouteConfigs.Hobby)]
        public IActionResult DeleteHobby(Guid HobbyID)
        {
            try
            {
                Hobby _hobby = new Hobby()
                {
                    HobbyId = HobbyID.ToString(),
                    HobbyName = ""
                };
                _hobbyBL.DeleteHobby(_hobby);
                Log.Information("Hobby Successfully deleted");
                return Ok("Hobby Deleted");
            }
            catch (System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Hobby + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }
    }
}
