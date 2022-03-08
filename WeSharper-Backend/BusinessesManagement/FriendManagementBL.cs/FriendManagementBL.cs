
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

        public Friend SendFriendRequest(Friend f_friend)
        {
            return _repo.AddFriend(f_friend);
        }
        public List<Friend> GetAllFriends()
        {
            return _repo.GetAllFriends();
        }
        public Friend GetAFriend(string userId)
        {
            Friend friends = _repo.GetAllFriends().FirstOrDefault(p => p.RequestedUserId == userId);
            if (friends == null)
            {
                throw new Exception("Friend not found");
            }
            else
            {
                return friends;
            }
        }
        public Friend UpdateFriendRequest(Friend f_friend)
        {
            return _repo.UpdateFriend(f_friend);
        }
        public Friend DeleteFriend(Friend f_friend);
        //bool ValidFriendName(string f_friend);
        public bool CheckDuplicateRequest(string friendName);
    }
}
