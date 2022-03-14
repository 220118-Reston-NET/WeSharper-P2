using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IFriendManagementDL
    {
        Task<List<Friend>> GetAllFriends();
        Task<Profile> GetFriendProfileByFriendID(string p_friendID);
        Task<List<Post>> GetFriendPostsByFriendID(string p_friendID);
        Task<Friend> AddFriend(string p_userID, string p_friendID);
        Task<Friend> RemoveFriend(string p_userID, string p_friendID);
        Task<Friend> AcceptFriend(string p_userID, string p_friendID);
        Task<List<Profile>> GetAllRecommenedFriendByUserID(string p_userID);
        Task<string> GetRelationshipByFriendID(string p_userID, string p_friendID);
        Task<string> GetUserNameForFriendID(string p_friendID);
    }
}