using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Reaction
    {
        public Reaction()
        {
            GroupPostCommentReacts = new HashSet<GroupPostCommentReact>();
            GroupPostReacts = new HashSet<GroupPostReact>();
            PostCommentReacts = new HashSet<PostCommentReact>();
            PostReacts = new HashSet<PostReact>();
        }

        public string ReactId { get; set; } = null!;
        public string? ReactName { get; set; }
        public string? ReactIcon { get; set; }

        public virtual ICollection<GroupPostCommentReact> GroupPostCommentReacts { get; set; }
        public virtual ICollection<GroupPostReact> GroupPostReacts { get; set; }
        public virtual ICollection<PostCommentReact> PostCommentReacts { get; set; }
        public virtual ICollection<PostReact> PostReacts { get; set; }
    }
}
