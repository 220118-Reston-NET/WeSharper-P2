using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class FriendManagementDL : IFriendManagementDL
    {
        private readonly WeSharperContext _context;

        public FriendManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Friend AddFriend(Friend f_friend)
        {
            _context.Friends.Add(f_friend);
            _context.SaveChanges();

            return f_friend;
        }

        public List<Friend> GetAllFriends()
        {
            return _context.Friends.ToList();
        }

        public Friend UpdateFriend(Friend f_friend)
        {
            Friend friendToUpdate = _context.Friends.Where(f => f.RequestedUserId == f_friend.RequestedUserId)
                                                    .Where( f => f.AcceptedUserId == f_friend.AcceptedUserId ).FirstOrDefault();
            if (friendToUpdate != null)
            {
                friendToUpdate.IsAccepted = f_friend.IsAccepted;
                friendToUpdate.Relationship = f_friend.Relationship;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No profiles found");
            }
            return f_friend;
        }

        public Friend DeleteFriend(Friend f_friend)
        {
            Friend friendToRemove = _context.Friends.Where(f => f.RequestedUserId == f_friend.RequestedUserId)
                                                    .Where( f => f.AcceptedUserId == f_friend.AcceptedUserId ).FirstOrDefault();
            if (friendToRemove != null)
            {
                _context.Friends.Remove(friendToRemove);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Profile not found. Friend could not be deleted.");
            }
            return f_friend;
        }
    }
}