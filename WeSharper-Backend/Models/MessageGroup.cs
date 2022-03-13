using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeSharper.Models
{
    public partial class MessageGroup
    {
        public MessageGroup()
        {
        }

        public MessageGroup(string name)
        {
            Name = name;
        }
        [Key]
        public string Name { get; set; }
        public ICollection<MessageConnection> MessageConnections { get; set; } = new List<MessageConnection>();
    }
}
