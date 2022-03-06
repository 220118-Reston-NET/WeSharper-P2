using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class PostReact
    {
        public string PostReactId { get; set; } = null!;
        public string? PostId { get; set; }
        public string? UserId { get; set; }
        public string? ReactId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual Reaction? React { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
