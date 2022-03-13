using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.APIPortal.Extensions;
using WeSharper.APIPortal.Helpers;
using WeSharper.APIPortal.Interfaces;
using WeSharper.Models;

namespace WeSharper.APIPortal.Implements
{
    public class MessageManagement : IMessageManagement
    {
        private readonly WeSharperContext _context;
        private readonly IMapper _mapper;
        public MessageManagement(WeSharperContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddGroup(MessageGroup p_messageGroup)
        {
            _context.MessageGroups.Add(p_messageGroup);
        }

        public void AddMessage(Message p_message)
        {
            _context.Messages.Add(p_message);
        }

        public void DeleteMessage(Message p_message)
        {
            _context.Messages.Remove(p_message);
        }

        public async Task<MessageConnection> GetConnection(string p_connectionId)
        {
            return await _context.MessageConnections.FindAsync(p_connectionId);
        }

        public async Task<MessageGroup> GetGroupForConnection(string p_connectionId)
        {
            return await _context.MessageGroups
                .Include(c => c.MessageConnections)
                .Where(c => c.MessageConnections.Any(x => x.ConnectionId == p_connectionId))
                .FirstOrDefaultAsync();
        }

        public async Task<Message> GetMessage(string p_messageID)
        {
            return await _context.Messages
                .Include(u => u.SenderUser)
                .Include(u => u.RecipientUser)
                .SingleOrDefaultAsync(x => x.MessageId.Equals(p_messageID));
        }

        public async Task<MessageGroup> GetMessageGroup(string p_groupName)
        {
            return await _context.MessageGroups
                .Include(x => x.MessageConnections)
                .FirstOrDefaultAsync(x => x.Name == p_groupName);
        }

        public async Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams p_messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .AsQueryable();

            query = p_messageParams.Container switch
            {
                "Inbox" => query.Where(u => u.RecipientUsername == p_messageParams.Username
                    && u.RecipientDeleted == false),
                "Outbox" => query.Where(u => u.SenderUsername == p_messageParams.Username
                    && u.SenderDeleted == false),
                _ => query.Where(u => u.RecipientUsername ==
                    p_messageParams.Username && u.RecipientDeleted == false && u.DateRead == null)
            };

            return await PagedList<MessageDTO>.CreateAsync(query, p_messageParams.PageNumber, p_messageParams.PageSize);

        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string p_currentUsername,
            string p_recipientUsername)
        {
            var messages = await _context.Messages
                .Where(m => m.RecipientUser.UserName == p_currentUsername && m.RecipientDeleted == false
                        && m.SenderUser.UserName == p_recipientUsername
                        || m.RecipientUser.UserName == p_recipientUsername
                        && m.SenderUser.UserName == p_currentUsername && m.SenderDeleted == false
                )
                .MarkUnreadAsRead(p_currentUsername)
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return messages;
        }

        public void RemoveConnection(MessageConnection p_connection)
        {
            _context.MessageConnections.Remove(p_connection);
        }
    }
}