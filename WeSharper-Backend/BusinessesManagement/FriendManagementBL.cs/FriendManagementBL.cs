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

        public Friend AcceptFriend(string p_userID, string p_friendID)
        {
            return _repo.AcceptFriend(p_userID, p_friendID);
        }

        public Friend AddFriend(string p_userID, string p_friendID)
        {
            return _repo.AddFriend(p_userID, p_friendID);
        }

        public List<Friend> GetAllFriendByUserID(string p_userID)
        {
            return GetAllFriends().FindAll(p => p.AcceptedUserId.Equals(p_userID) && p.IsAccepted
                                            || p.RequestedUserId.Equals(p_userID) && p.IsAccepted);
        }

        public List<Friend> GetAllFriends()
        {
            return _repo.GetAllFriends();
        }

        public List<Friend> GetAllIncomingFriendByUserID(string p_userID)
        {
            return GetAllFriends().FindAll(p => p.AcceptedUserId.Equals(p_userID)
                                            && p.IsAccepted.Equals(false)
                                            && p.Relationship != null);
        }

        public List<Friend> GetAllOutcomingFriendByUserID(string p_userID)
        {
            return GetAllFriends().FindAll(p => p.RequestedUserId.Equals(p_userID)
                                            && p.IsAccepted.Equals(false)
                                            && p.Relationship != null);
        }

        public List<Profile> GetAllRecommenedFriendByUserID(string p_userID)
        {
            return _repo.GetAllRecommenedFriendByUserID(p_userID);
        }

        public List<Post> GetFriendPostsByFriendID(string p_friendID)
        {
            return _repo.GetFriendPostsByFriendID(p_friendID);
        }

        public Profile GetFriendProfileByFriendID(string p_friendID)
        {
            return _repo.GetFriendProfileByFriendID(p_friendID);
        }

        public string GetRelationshipByFriendID(string p_userID, string p_friendID)
        {
            return _repo.GetRelationshipByFriendID(p_userID, p_friendID);
        }

        public string GetUserNameForFriendID(string p_friendID)
        {
            return _repo.GetUserNameForFriendID(p_friendID);
        }

        public Friend RemoveFriend(string p_userID, string p_friendID)
        {
            return _repo.RemoveFriend(p_userID, p_friendID);
        }
    }
}