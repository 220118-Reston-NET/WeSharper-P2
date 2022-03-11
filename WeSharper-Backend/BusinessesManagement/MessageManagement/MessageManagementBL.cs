using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class MessageManagementBL : IMessageManagementBL
    {
        private readonly IMessageManagementDL _repo;

        public MessageManagementBL(IMessageManagementDL repo)
        {
            _repo = repo;
        }

        public Message AddMessage(Message p_message)
        {
            p_message.MessageId = Guid.NewGuid().ToString();
            p_message.MessageSent = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_message.SenderDeleted = false;
            p_message.RecipientDeleted = false;

            return _repo.AddMessage(p_message);
        }
    }
}