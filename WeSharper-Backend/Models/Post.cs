using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Post
    {
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostReacts = new HashSet<PostReact>();
        }

        public string PostId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? PostContent { get; set; }
        public string? PostPhoto { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
        public virtual ICollection<PostReact> PostReacts { get; set; }
    }
}
