using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IFriendManagementBL
    {
        List<Friend> GetAllFriends();
        List<Friend> GetAllFriendByUserID(string p_userID);
        List<Profile> GetAllFriendProfiles();
        Profile GetFriendProfileByFriendID(string p_friendID);
        Friend AddFriend(string p_userID, string p_friendID);
        Friend GetRelationship(string p_userID, string p_friendID);
    }
}