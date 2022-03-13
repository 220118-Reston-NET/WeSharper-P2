using System.Collections.Generic;
using System.Threading.Tasks;
using WeSharper.APIPortal.DataTransferObjects;
using WeSharper.APIPortal.Helpers;
using WeSharper.Models;

namespace WeSharper.APIPortal.Interfaces
{
    public interface IMessageManagement
    {
        void AddGroup(MessageGroup p_messageGroup);
        void RemoveConnection(MessageConnection p_connection);
        Task<MessageConnection> GetConnection(string p_connectionId);
        Task<MessageGroup> GetMessageGroup(string p_groupName);
        Task<MessageGroup> GetGroupForConnection(string p_connectionId);
        void AddMessage(Message p_message);
        void DeleteMessage(Message p_message);
        Task<Message> GetMessage(string p_messageId);
        Task<PagedList<MessageDTO>> GetMessagesForUser(MessageParams p_messagemessageParams);
        Task<IEnumerable<MessageDTO>> GetMessageThread(string p_currentUsername, string p_recipientUsername);
    }
}