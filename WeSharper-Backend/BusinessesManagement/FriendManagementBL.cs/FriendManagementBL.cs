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
            // check to see if friend request is already in database
            try
            {
                Friend tempFriend = CheckFriend(f_friend.RequestedUserId,f_friend.AcceptedUserId);
                return _repo.AddFriend(f_friend);
            }
            catch
            {
                throw new Exception("Error, friend request already sent/recieved");
            }

            
        }
        public List<Friend> GetAllFriends()
        {
            List<Friend> allFriends = _repo.GetAllFriends();
            if( !allFriends.Any() )
            {
                throw new Exception("No one has any friends");
            }
            else
            {
                return allFriends;
            }
        }
        
        public List<Friend> GetUserFriends(string userId)
        {
            List<Friend> friends = _repo.GetAllFriends().Where(p => (p.RequestedUserId == userId) && (p.IsAccepted == true) ||
                                                                    (p.AcceptedUserId == userId)  && (p.IsAccepted == true)).ToList();
            if (!friends.Any() )
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
            
            try
            {
                Friend tempFriend = CheckFriend(f_friend.RequestedUserId,f_friend.AcceptedUserId);
                f_friend.RelationshipId = tempFriend.RelationshipId;
                return(_repo.UpdateFriend(f_friend));
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public Friend DeleteFriend(string userId, string friendId)
        {
            try
            {
                Friend tempFriend = CheckFriend(userId,friendId);
                return(_repo.DeleteFriend(tempFriend));
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public List<Friend> GetUnacceptedSentRequests(string userId)
        {
            List<Friend> friends = _repo.GetAllFriends().Where(p => (p.RequestedUserId == userId) && (p.IsAccepted == false)).ToList();
            if (!friends.Any())
            {
                throw new Exception("No friend request sent");
            }
            else
            {
                return friends;
            }
        }
        public List<Friend> GetUserPendingFriendRequests(string userId)
        {
            List<Friend> friends = _repo.GetAllFriends().Where(p => (p.AcceptedUserId == userId) && (p.IsAccepted == false)).ToList();
            if (!friends.Any())
            {
                throw new Exception("No pending friend requests");
            }
            else
            {
                return friends;
            }
        }
        
        
        public Friend CheckFriend(string userId, string friendId)
        {
            Friend friend = _repo.GetAllFriends().FirstOrDefault(p => (p.RequestedUserId == userId) && (p.AcceptedUserId == friendId) ||
                                                                            (p.RequestedUserId == friendId)  && (p.AcceptedUserId == userId));
            if(friend == null)
            {
                throw new Exception("User has no relations with this person");
            }
            else
            {
                return friend;
            }
        }
        /*
        bool ValidFriendName(string f_friend);
        
        */
    }
}