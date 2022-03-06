using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Profile
    {
        public string ProfileId { get; set; } = null!;
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ApplicationUser? User { get; set; }
    }
}
