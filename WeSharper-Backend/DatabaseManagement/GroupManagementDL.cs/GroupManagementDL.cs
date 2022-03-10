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
    }
}