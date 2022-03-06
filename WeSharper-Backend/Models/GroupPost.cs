using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class GroupPost
    {
        public GroupPost()
        {
            GroupPostComments = new HashSet<GroupPostComment>();
            GroupPostReacts = new HashSet<GroupPostReact>();
        }

        public string GroupPostId { get; set; } = null!;
        public string? GroupId { get; set; }
        public string? UserId { get; set; }
        public string? PostContent { get; set; }
        public string? PostPhoto { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Group? Group { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<GroupPostComment> GroupPostComments { get; set; }
        public virtual ICollection<GroupPostReact> GroupPostReacts { get; set; }
    }
}
