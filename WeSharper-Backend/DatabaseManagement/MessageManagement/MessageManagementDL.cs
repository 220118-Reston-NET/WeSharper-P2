using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class MessageManagementDL : IMessageManagementDL
    {
        private readonly WeSharperContext _context;

        public MessageManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Message AddMessage(Message p_message)
        {
            _context.Add(p_message);
            _context.SaveChanges();

            return p_message;
        }
    }
}