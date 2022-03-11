using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IFriendManagementDL
    {
        List<Friend> GetAllFriends();
        List<Profile> GetAllFriendProfiles();
        Profile GetFriendProfileByFriendID(string p_friendID);
        Friend AddFriend(string p_userID, string p_friendID);
        Friend GetRelationship(string p_userID, string p_friendID);
    }
}