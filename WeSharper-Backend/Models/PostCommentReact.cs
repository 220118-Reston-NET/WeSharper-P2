using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class PostCommentReact
    {
        public string PostCommentReactId { get; set; } = null!;
        public string? CommentId { get; set; }
        public string? UserId { get; set; }
        public string? ReactId { get; set; }

        public virtual PostComment? Comment { get; set; }
        public virtual Reaction? React { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
