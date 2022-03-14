using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IFriendManagementBL
    {
        Task<List<Friend>> GetAllFriends();
        Task<List<Friend>> GetAllFriendByUserID(string p_userID);
        Task<List<Friend>> GetAllIncomingFriendByUserID(string p_userID);
        Task<List<Friend>> GetAllOutcomingFriendByUserID(string p_userID);
        Task<List<Profile>> GetAllRecommenedFriendByUserID(string p_userID);
        Task<Profile> GetFriendProfileByFriendID(string p_friendID);
        Task<List<Post>> GetFriendPostsByFriendID(string p_friendID);
        Task<Friend> AddFriend(string p_userID, string p_friendID);
        Task<Friend> RemoveFriend(string p_userID, string p_friendID);
        Task<Friend> AcceptFriend(string p_userID, string p_friendID);
        Task<string> GetRelationshipByFriendID(string p_userID, string p_friendID);
        Task<string> GetUserNameForFriendID(string p_friendID);
    }
}