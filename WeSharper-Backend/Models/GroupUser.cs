using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class GroupUser
    {
        public string? GroupId { get; set; }
        public string? UserId { get; set; }
        public bool IsBanned { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Group? Group { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
