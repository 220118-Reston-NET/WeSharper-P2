using System;
using System.Collections.Generic;

namespace WeSharper.Models
{
    public partial class Userhobby
    {
        public string? UserId { get; set; }
        public string? HobbyId { get; set; }

        public virtual Hobby? Hobby { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
