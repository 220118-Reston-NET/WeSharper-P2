using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Message
    {
        public string MessageId { get; set; } = null!;
        public string? SenderUserId { get; set; }
        public string? RecipientUserId { get; set; }
        public string? Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? MessageSent { get; set; }
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }

        public virtual ApplicationUser? RecipientUser { get; set; }
        public virtual ApplicationUser? SenderUser { get; set; }
    }
}
