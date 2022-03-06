using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Group
    {
        public Group()
        {
            GroupPostCommentReacts = new HashSet<GroupPostCommentReact>();
            GroupPostComments = new HashSet<GroupPostComment>();
            GroupPostReacts = new HashSet<GroupPostReact>();
            GroupPosts = new HashSet<GroupPost>();
        }

        public string GroupId { get; set; } = null!;
        public string? GroupManagerId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupPicture { get; set; }
        public string? Description { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ApplicationUser? GroupManager { get; set; }
        public virtual ICollection<GroupPostCommentReact> GroupPostCommentReacts { get; set; }
        public virtual ICollection<GroupPostComment> GroupPostComments { get; set; }
        public virtual ICollection<GroupPostReact> GroupPostReacts { get; set; }
        public virtual ICollection<GroupPost> GroupPosts { get; set; }
    }
}
