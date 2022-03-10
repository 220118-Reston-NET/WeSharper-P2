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
            Friend newFriend = new Friend(){
                RelationshipId = f_friend.RelationshipId,
                RequestedUserId = f_friend.RequestedUserId,
                AcceptedUserId = f_friend.AcceptedUserId,
                IsAccepted = false,
                Relationship = null,
                CreatedAt = null
            };
            _context.Friends.Add(newFriend);
            _context.SaveChanges();

            return newFriend;
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
                if(friendToUpdate.IsAccepted == false && f_friend.IsAccepted == true)
                {
                    friendToUpdate.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
                    friendToUpdate.Relationship = f_friend.Relationship;
                }
                if(friendToUpdate.IsAccepted == true && f_friend.IsAccepted == false)
                {
                    friendToUpdate.CreatedAt = null;
                    friendToUpdate.Relationship = null;
                }
                friendToUpdate.IsAccepted = f_friend.IsAccepted;
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