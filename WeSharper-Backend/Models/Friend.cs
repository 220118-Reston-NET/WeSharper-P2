using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Friend
    {
        public string RelationshipId { get; set; } = null!;
        public string? RequestedUserId { get; set; }
        public string? AcceptedUserId { get; set; }
        public bool IsAccepted { get; set; }
        public string? Relationship { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ApplicationUser? AcceptedUser { get; set; }
        public virtual ApplicationUser? RequestedUser { get; set; }
    }
}
