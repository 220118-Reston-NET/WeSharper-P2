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

        public Friend AddFriend(string p_userID, string p_friendID)
        {
            return _repo.AddFriend(p_userID, p_friendID);
        }

        public List<Friend> GetAllFriendByUserID(string p_userID)
        {
            return GetAllFriends().FindAll(p => p.AcceptedUserId.Equals(p_userID) && p.IsAccepted
                                            || p.RequestedUserId.Equals(p_userID) && p.IsAccepted);
        }

        public List<Profile> GetAllFriendProfiles()
        {
            return _repo.GetAllFriendProfiles();
        }

        public List<Friend> GetAllFriends()
        {
            return _repo.GetAllFriends();
        }

        public Profile GetFriendProfileByFriendID(string p_friendID)
        {
            return _repo.GetFriendProfileByFriendID(p_friendID);
        }

        public Friend GetRelationship(string p_userID, string p_friendID)
        {
            return _repo.GetRelationship(p_userID, p_friendID);
        }
    }
}