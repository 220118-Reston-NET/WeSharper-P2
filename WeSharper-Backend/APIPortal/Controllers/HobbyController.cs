using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObject;
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
        public HobbyController(IHobbyManagementBL h_hobbyBL){
            _hobbyBL = h_hobbyBL;
        }
        // PUT: api/Hobby
        [HttpPost(RouteConfigs.Hobby)]
        public IActionResult AddNewHobby([FromBody] HobbyForm hobbyForm)
        {
            try
            {
                Regex hobbyRegex =  new Regex(@"^[a-zA-Z]$");
                if( hobbyRegex.IsMatch(hobbyForm.HobbyName) )
                {
                    Log.Warning("Route: " + RouteConfigs.Register);
                    return BadRequest(new { Result = "Hobby is in the incorrect format" });
                }
                var newId = Guid.NewGuid().ToString();
                Hobby h = new Hobby(){
                    HobbyId = newId,
                    HobbyName = hobbyForm.HobbyName
                };
                _hobbyBL.AddNewHobby(h);
                Log.Information("Hobby Successfully created");
                return Created("Has created",h);
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Register + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }
        // POST: api/Hobby
        [HttpPut(RouteConfigs.Hobby)]
        public IActionResult Put([FromBody] HobbyForm hobbyForm, string HobbyID)
        {
            try{
                Hobby h = new Hobby(){
                    HobbyId = HobbyID,
                    HobbyName = hobbyForm.HobbyName
                };
                _hobbyBL.UpdateHobby(h);
                Log.Information("Hobby Successfully updated");
                return Ok("Hobby Updated");
            }
            catch(System.Exception exe)
            {
                return Conflict(exe.Message);
            }
        }
        // DELETE: api/Hobby/5
        [HttpDelete(RouteConfigs.Hobby)]
        public IActionResult Delete(Guid HobbyID)
        {
            try{
                Hobby h = new Hobby(){
                    HobbyId = HobbyID.ToString(),
                    HobbyName = ""
                };
                _hobbyBL.DeleteHobby(h);
                Log.Information("Hobby Successfully deleted");
                return Ok("Hobby Deleted");
            }
            catch(System.Exception exe)
            {
                Log.Warning("Route:" + RouteConfigs.Register + ": " + exe.Message);
                return Conflict(exe.Message);
            }
        }
    }
}
