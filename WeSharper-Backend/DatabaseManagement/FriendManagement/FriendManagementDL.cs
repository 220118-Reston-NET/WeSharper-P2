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

        public Friend AddFriend(string p_userID, string p_friendID)
        {
            Friend _currentRelationship = _context.Friends.FirstOrDefault(p => p.AcceptedUserId.Equals(p_userID)
                                                                                && p.RequestedUserId.Equals(p_friendID)
                                                                            || p.AcceptedUserId.Equals(p_friendID)
                                                                                && p.RequestedUserId.Equals(p_userID));
            if (_currentRelationship != null && _currentRelationship.IsAccepted)
            {
                throw new Exception("You already added this friend!");
            }
            else if (_currentRelationship != null && _currentRelationship.IsAccepted)
            {

            }

            return _currentRelationship;
        }

        public List<Profile> GetAllFriendProfiles()
        {
            return _context.Profiles.ToList();
        }

        public List<Friend> GetAllFriends()
        {
            return _context.Friends.ToList();
        }

        public Profile GetFriendProfileByFriendID(string p_friendID)
        {
            return _context.Profiles.FirstOrDefault(p => p.UserId.Equals(p_friendID));
        }

        public Friend GetRelationship(string p_userID, string p_friendID)
        {
            return _context.Friends.FirstOrDefault(p => p.AcceptedUserId.Equals(p_userID) && p.RequestedUserId.Equals(p_friendID)
                                                    || p.AcceptedUserId.Equals(p_friendID) && p.RequestedUserId.Equals(p_userID));
        }
    }
}