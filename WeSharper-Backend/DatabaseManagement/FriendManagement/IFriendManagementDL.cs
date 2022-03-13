using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IFriendManagementDL
    {
        List<Friend> GetAllFriends();
        Profile GetFriendProfileByFriendID(string p_friendID);
        List<Post> GetFriendPostsByFriendID(string p_friendID);
        Friend AddFriend(string p_userID, string p_friendID);
        Friend RemoveFriend(string p_userID, string p_friendID);
        Friend AcceptFriend(string p_userID, string p_friendID);
        List<Profile> GetAllRecommenedFriendByUserID(string p_userID);
        string GetRelationshipByFriendID(string p_userID, string p_friendID);
    }
}