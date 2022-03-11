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
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.Models;

namespace APIPortal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageManagementBL _messageBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;

        public MessagesController(IMessageManagementBL messageBL,
                                    IHttpContextAccessor httpContextAccessor,
                                    UserManager<ApplicationUser> userManager)
        {
            _messageBL = messageBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
        }

        [Authorize(Roles = "User")]
        [HttpPost(RouteConfigs.CreateMessage)]
        public async Task<IActionResult> CreateMessage(CreateMessage p_createMessage)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                if (p_createMessage.RecipientUserId.Equals(p_userID))
                {
                    return BadRequest("You cannot send message to yourself!");
                }

                Message message = new Message()
                {
                    SenderUserId = p_userID,
                    RecipientUserId = p_createMessage.RecipientUserId,
                    Content = p_createMessage.Content
                };

                return Created("Created new message successfully!", _messageBL.AddMessage(message));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route: " + RouteConfigs.CreateMessage);
                Log.Warning(e.Message);

                return BadRequest(new { Results = "Send message failed!" });
            }

        }
    }
}
