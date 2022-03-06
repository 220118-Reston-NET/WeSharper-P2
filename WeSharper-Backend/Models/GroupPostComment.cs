using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class GroupPostComment
    {
        public GroupPostComment()
        {
            GroupPostCommentReacts = new HashSet<GroupPostCommentReact>();
        }

        public string GroupPostCommentId { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? GroupPostId { get; set; }
        public string? UserId { get; set; }
        public string? GroupPostComment1 { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Group? Group { get; set; }
        public virtual GroupPost? GroupPost { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<GroupPostCommentReact> GroupPostCommentReacts { get; set; }
    }
}
