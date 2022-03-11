using WeSharper.Models;

namespace WeSharper.APIPortal.DataTransferObjects
{
    public class FriendProfile
    {
        public Profile FriendProfileInformation { get; set; }
        public Friend Relationship { get; set; }
    }
}