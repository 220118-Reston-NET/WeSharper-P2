using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeSharper.Models
{
    public partial class MessageConnection
    {
        public MessageConnection()
        {
        }

        public MessageConnection(string connectionId, string username)
        {
            ConnectionId = connectionId;
            Username = username;
        }
        [Key]
        public string ConnectionId { get; set; }
        public string Username { get; set; }
    }
}
