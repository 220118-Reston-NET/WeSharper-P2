using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class GroupManagementDL : IGroupManagementDL
    {
        private readonly WeSharperContext _context;

        public GroupManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Group AddGroup(Group g_group)
        {
            Group newGroup = new Group(){
                GroupId = Guid.NewGuid().ToString(),
                GroupManagerId = g_group.GroupManagerId,
                GroupName = g_group.GroupName,
                GroupPicture = g_group.GroupPicture,
                Description = g_group.Description,
                
            };
            _context.Groups.Add(newGroup);
            _context.SaveChanges();

            return newGroup;
        }

        public List<Group> GetAllGroups()
        {
            return _context.Groups.ToList();
        }


        public Group UpdateGroup(Group g_group, string userId)
        {
            Group groupToUpdate = _context.Groups.Where(g => g.GroupId == g_group.GroupId).FirstOrDefault();
            if (groupToUpdate != null)
            {
                if(groupToUpdate.GroupManagerId == userId)
                {
                    if(g_group.GroupManagerId != null)
                        groupToUpdate.GroupManagerId = g_group.GroupManagerId;
                    if(g_group.GroupName != null)
                        groupToUpdate.GroupName = g_group.GroupName;
                    groupToUpdate.GroupPicture = g_group.GroupPicture;
                    groupToUpdate.Description = g_group.Description;
                }
                else
                {
                    throw new Exception("User does not have perms to update");
                }

                _context.SaveChanges();
                return groupToUpdate;
            }
            else
            {
                throw new Exception("No groups found with that manager");
            }
            
        }

        public Group DeleteGroup(string groupId, string groupManagerId)
        {
            Group groupToRemove = _context.Groups.Where(g => (g.GroupId == groupId)  ).FirstOrDefault();
            if (groupToRemove != null)
            {
                if(groupToRemove.GroupManagerId == groupManagerId){
                    groupToRemove.IsActivated = false;
                    _context.SaveChanges();
                    return groupToRemove;
                }
                else
                {
                    throw new Exception("User does not have permission to delete group");
                }
            }
            else
            {
                throw new Exception("Profile not found. Group could not be deleted.");
            }
        }




        public GroupUser SendNewGroupUserRequest(GroupUser g_groupUser)
        {   
            g_groupUser.User = _context.Users.FirstOrDefault(u => (u.Id == g_groupUser.UserId));
            g_groupUser.Group = _context.Groups.FirstOrDefault(g => (g.GroupId == g_groupUser.GroupId));
            _context.GroupUsers.Add(g_groupUser);
            _context.SaveChanges();
            return g_groupUser;
        }
        

        public List<GroupUser> GetAllGroupUsers()
        {
            return _context.GroupUsers.ToList();
        }
        public List<GroupUser> GetGroupApprovedUsersInGroup(string groupId)
        { 
            return _context.GroupUsers.Where(g => (g.GroupId == groupId) && (g.IsApproved == true ) && (g.IsBanned == false)).ToList();
        }

        public GroupUser UpdateGroupUser(GroupUser g_groupUser)
        {
            try
            {
                GroupUser groupUserToUpdate= _context.GroupUsers.FirstOrDefault(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId) );

                _context.SaveChanges();
                return groupUserToUpdate;
            }
            catch(System.Exception exe)
            {
                throw new Exception("User could not be found in the group");
            }  
        }
        public GroupUser BanGroupUser(GroupUser g_groupUser)
        {
            GroupUser groupUser = _context.GroupUsers.FirstOrDefault(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));
            groupUser.IsBanned = true;
            _context.SaveChanges();
            return groupUser;
        }
        public GroupUser UnbanGroupUser(GroupUser g_groupUser)
        {
            GroupUser groupUser = _context.GroupUsers.FirstOrDefault(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));
            groupUser.IsBanned = false;
            _context.SaveChanges();
            return groupUser;
        }
        public GroupUser DeleteGroupUser(GroupUser g_groupUser)
        {
            GroupUser groupUserToRemove = _context.GroupUsers.FirstOrDefault(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));
            Console.WriteLine("crymaru");
            _context.Remove(groupUserToRemove); 
            return groupUserToRemove;
        } 

        
    }
}
