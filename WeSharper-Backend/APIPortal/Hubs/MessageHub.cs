using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.APIPortal.Extensions;
using WeSharper.APIPortal.Interfaces;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.APIPortal.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _tracker;
        private readonly IProfileManagementBL _profileBL;
        private readonly IUnitOfWork _unitOfWork;
        public MessageHub(IMapper mapper, IUnitOfWork unitOfWork,
                            IHubContext<PresenceHub> presenceHub,
                            PresenceTracker tracker,
                            IProfileManagementBL profileBL)
        {
            _unitOfWork = unitOfWork;
            _tracker = tracker;
            _presenceHub = presenceHub;
            _mapper = mapper;
            _profileBL = profileBL;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);
            await Clients.Group(groupName).SendAsync("UpdatedGroup", group);

            var messages = await _unitOfWork.MessageManagement.
                GetMessageThread(Context.User.GetUsername(), otherUser);

            if (_unitOfWork.HasChanges()) await _unitOfWork.Complete();

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await Clients.Group(group.Name).SendAsync("UpdatedGroup", group);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(CreateMessage createMessage)
        {
            var username = Context.User.GetUsername();

            if (username == createMessage.RecipientUsername.ToLower())
                throw new HubException("You cannot send messages to yourself");

            var sender = await _profileBL.GetUserByUserName(username);
            var recipient = await _profileBL.GetUserByUserName(createMessage.RecipientUsername);

            if (recipient == null) throw new HubException("Not found user");

            var message = new Message
            {
                SenderUser = sender,
                RecipientUser = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessage.Content
            };
            Console.WriteLine(message.RecipientUserId);

            var groupName = GetGroupName(sender.UserName, recipient.UserName);

            var group = await _unitOfWork.MessageManagement.GetMessageGroup(groupName);

            if (group.MessageConnections.Any(x => x.Username == recipient.UserName))
            {
                message.DateRead = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            }
            else
            {
                var connections = await _tracker.GetConnectionsForUser(recipient.UserName);
                if (connections != null)
                {
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived",
                        new { username = sender.UserName, userId = sender.Id });
                }
            }

            _unitOfWork.MessageManagement.AddMessage(message);

            if (await _unitOfWork.Complete())
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDTO>(message));
            }
        }

        private async Task<MessageGroup> AddToGroup(string groupName)
        {
            var group = await _unitOfWork.MessageManagement.GetMessageGroup(groupName);
            var connection = new MessageConnection(Context.ConnectionId, Context.User.GetUsername());

            if (group == null)
            {
                group = new MessageGroup(groupName);
                _unitOfWork.MessageManagement.AddGroup(group);
            }

            group.MessageConnections.Add(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Failed to join group");
        }

        private async Task<MessageGroup> RemoveFromMessageGroup()
        {
            var group = await _unitOfWork.MessageManagement.GetGroupForConnection(Context.ConnectionId);
            var connection = group.MessageConnections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            _unitOfWork.MessageManagement.RemoveConnection(connection);
            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Failed to remove from group");
        }

        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }
    }
}