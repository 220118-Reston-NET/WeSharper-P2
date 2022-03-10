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
            List<Friend> tempFriendList = _repo.GetAllFriends().Where(p => (p.RequestedUserId == f_friend.RequestedUserId) && (p.AcceptedUserId == f_friend.AcceptedUserId) ||
                                                                           (p.RequestedUserId == f_friend.AcceptedUserId)  && (p.AcceptedUserId == f_friend.RequestedUserId)).ToList();
            if(tempFriendList.Any())
            {
                throw new Exception("Error, friend request already sent/recieved");
            }

            return _repo.AddFriend(f_friend);
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
                try
                {
                    return(_repo.UpdateFriend(f_friend));
                }
                catch(System.Exception exe)
                {
                    Friend _newFriend = new Friend()
                    {
                        RelationshipId = null,
                        RequestedUserId = f_friend.AcceptedUserId,
                        AcceptedUserId = f_friend.RequestedUserId,
                        IsAccepted = f_friend.IsAccepted,
                        Relationship = f_friend.Relationship,
                        CreatedAt = null
                    };
                    return (_repo.UpdateFriend(_newFriend));
                }
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
                try
                {
                    Friend _newFriend = new Friend()
                    {
                        RelationshipId = null,
                        RequestedUserId = userId,
                        AcceptedUserId = friendId,
                        IsAccepted = false,
                        Relationship = null,
                        CreatedAt = null
                    };
                    return(_repo.DeleteFriend(_newFriend));
                }
                catch(System.Exception exe)
                {
                    Friend _newFriend = new Friend()
                    {
                        RelationshipId = null,
                        RequestedUserId = friendId,
                        AcceptedUserId = userId,
                        IsAccepted = false,
                        Relationship = null,
                        CreatedAt = null
                    };
                    return (_repo.DeleteFriend(_newFriend));
                }
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        /*
        
        bool ValidFriendName(string f_friend);
        
        public List<Friend> GetFriendsRequests(string userId)
        {
            List<Friend> friends = _repo.GetAllFriends().Select(p => (p.RequestedUserId == userId) && (p.IsAccepted == true));
            if (friends == null)
            {
                throw new Exception("Friend not found");
            }
            else
            {
                return friends.Concat(friends2) ;
            }
        } */
    }
}