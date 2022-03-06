using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class GroupPostCommentReact
    {
        public string GroupPostCommentReactId { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? GroupPostCommentId { get; set; }
        public string? UserId { get; set; }
        public string? ReactId { get; set; }

        public virtual Group? Group { get; set; }
        public virtual GroupPostComment? GroupPostComment { get; set; }
        public virtual Reaction? React { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
