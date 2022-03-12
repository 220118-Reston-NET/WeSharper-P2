using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IFriendManagementBL
    {
        List<Friend> GetAllFriends();
        List<Friend> GetAllFriendByUserID(string p_userID);
        List<Friend> GetAllIncomingFriendByUserID(string p_userID);
        List<Friend> GetAllOutcomingFriendByUserID(string p_userID);
        Profile GetFriendProfileByFriendID(string p_friendID);
        List<Post> GetFriendPostsByFriendID(string p_friendID);
        Friend AddFriend(string p_userID, string p_friendID);
        Friend RemoveFriend(string p_userID, string p_friendID);
        Friend AcceptFriend(string p_userID, string p_friendID);
    }
}