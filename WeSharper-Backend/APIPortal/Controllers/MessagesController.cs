using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeSharper.APIPortal.Consts;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.APIPortal.Extensions;
using WeSharper.APIPortal.Helpers;
using WeSharper.APIPortal.Interfaces;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.Models;

namespace APIPortal.Controllers
{
    [Authorize]
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public MessagesController(IHttpContextAccessor httpContextAccessor,
                                    UserManager<ApplicationUser> userManager,
                                    IMapper mapper,
                                    IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["unique_name"].ToString();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();

            var messages = await _unitOfWork.MessageManagement.GetMessagesForUser(messageParams);

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
                messages.TotalCount, messages.TotalPages);

            return messages;
        }

        [Authorize(Roles = "User")]
        [HttpDelete(RouteConfigs.DeleteMessage)]
        public async Task<ActionResult> DeleteMessage(string p_messageId)
        {
            var username = User.GetUsername();

            var message = await _unitOfWork.MessageManagement.GetMessage(p_messageId);

            if (message.SenderUser.UserName != username && message.RecipientUser.UserName != username)
                return Unauthorized();

            if (message.SenderUser.UserName == username) message.SenderDeleted = true;

            if (message.RecipientUser.UserName == username) message.RecipientDeleted = true;

            if (message.SenderDeleted && message.RecipientDeleted)
                _unitOfWork.MessageManagement.DeleteMessage(message);

            if (await _unitOfWork.Complete()) return Ok();

            return BadRequest("Problem deleting the message");
        }
    }
}
