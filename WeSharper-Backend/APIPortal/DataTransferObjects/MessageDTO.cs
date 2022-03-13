using System;
using System.Text.Json.Serialization;

namespace WeSharper.APIPortal.DataTransferObjects
{
    public class MessageDTO
    {
        public string MessageId { get; set; } = null!;
        public string? SenderUserId { get; set; }
        public string SenderUsername { get; set; }
        public string? SenderPhotoUrl { get; set; }
        public string? RecipientUserId { get; set; }
        public string RecipientUsername { get; set; }
        public string? RecipientPhotoUrl { get; set; }

        public string? Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime? MessageSent { get; set; }

        [JsonIgnore]
        public bool SenderDeleted { get; set; }
        [JsonIgnore]
        public bool RecipientDeleted { get; set; }
    }
}