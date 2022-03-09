namespace WeSharper.APIPortal.DataTransferObjects
{
    public class FriendForm
    {
        public string? RequestedUserId { get; set; }
        public string? AcceptedUserId { get; set; }
        public bool IsAccepted { get; set; }
        public string? Relationship { get; set; }
    }
}