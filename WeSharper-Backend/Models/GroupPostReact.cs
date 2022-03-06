using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class GroupPostReact
    {
        public string GroupPostReactId { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? GroupPostId { get; set; }
        public string? UserId { get; set; }
        public string? ReactId { get; set; }

        public virtual Group? Group { get; set; }
        public virtual GroupPost? GroupPost { get; set; }
        public virtual Reaction? React { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
