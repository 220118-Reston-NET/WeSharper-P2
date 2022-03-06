using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class PostComment
    {
        public PostComment()
        {
            PostCommentReacts = new HashSet<PostCommentReact>();
        }

        public string CommentId { get; set; } = null!;
        public string? PostId { get; set; }
        public string? UserId { get; set; }
        public string? PostComment1 { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Post? Post { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<PostCommentReact> PostCommentReacts { get; set; }
    }
}
