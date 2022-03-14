using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class FriendManagementBL : IFriendManagementBL
    {
        private readonly IFriendManagementDL _repo;

        public FriendManagementBL(IFriendManagementDL repo)
        {
            _repo = repo;
        }

        public async Task<Friend> AcceptFriend(string p_userID, string p_friendID)
        {
            return await _repo.AcceptFriend(p_userID, p_friendID);
        }

        public async Task<Friend> AddFriend(string p_userID, string p_friendID)
        {
            return await _repo.AddFriend(p_userID, p_friendID);
        }

        public async Task<List<Friend>> GetAllFriendByUserID(string p_userID)
        {
            List<Friend> _result = await GetAllFriends();
            return _result.FindAll(p => p.AcceptedUserId.Equals(p_userID) && p.IsAccepted
                                            || p.RequestedUserId.Equals(p_userID) && p.IsAccepted);
        }

        public async Task<List<Friend>> GetAllFriends()
        {
            return await _repo.GetAllFriends();
        }

        public async Task<List<Friend>> GetAllIncomingFriendByUserID(string p_userID)
        {
            List<Friend> _result = await GetAllFriends();
            return _result.FindAll(p => p.AcceptedUserId.Equals(p_userID)
                                            && p.IsAccepted.Equals(false)
                                            && p.Relationship != null);
        }

        public async Task<List<Friend>> GetAllOutcomingFriendByUserID(string p_userID)
        {
            List<Friend> _result = await GetAllFriends();
            return _result.FindAll(p => p.RequestedUserId.Equals(p_userID)
                                            && p.IsAccepted.Equals(false)
                                            && p.Relationship != null);
        }

        public async Task<List<Profile>> GetAllRecommenedFriendByUserID(string p_userID)
        {
            return await _repo.GetAllRecommenedFriendByUserID(p_userID);
        }

        public async Task<List<Post>> GetFriendPostsByFriendID(string p_friendID)
        {
            return await _repo.GetFriendPostsByFriendID(p_friendID);
        }

        public async Task<Profile> GetFriendProfileByFriendID(string p_friendID)
        {
            return await _repo.GetFriendProfileByFriendID(p_friendID);
        }

        public async Task<string> GetRelationshipByFriendID(string p_userID, string p_friendID)
        {
            return await _repo.GetRelationshipByFriendID(p_userID, p_friendID);
        }

        public async Task<string> GetUserNameForFriendID(string p_friendID)
        {
            return await _repo.GetUserNameForFriendID(p_friendID);
        }

        public async Task<Friend> RemoveFriend(string p_userID, string p_friendID)
        {
            return await _repo.RemoveFriend(p_userID, p_friendID);
        }
    }
}