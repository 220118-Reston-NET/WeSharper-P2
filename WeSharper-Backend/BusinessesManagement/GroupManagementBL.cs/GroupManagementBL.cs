using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class GroupManagementBL : IGroupManagementBL
    {
        private readonly IGroupManagementDL _repo;

        public GroupManagementBL(IGroupManagementDL repo)
        {
            _repo = repo;
        }

        public Group AddNewGroup(Group g_group)
        {
            return _repo.AddGroup(g_group);
        }
        public List<Group> GetAllGroups()
        {
            List<Group> allGroups = _repo.GetAllGroups();
            if( !allGroups.Any() )
            {
                throw new Exception("No one has any groups");
            }
            else
            {
                return allGroups;
            }
        }
        
        /*public Group GetGroupFromName(string groupName)
        {
            Group group = _repo.GetAllGroups().FirstOrDefault(g => (g.GroupName == groupName));
            if (group == null )
            {
                throw new Exception("Group not found"); 
            }
            else
            {
                return group;
            }
        } */
        public Group CheckGroupId(string groupId)
        {
            Group group = _repo.GetAllGroups().FirstOrDefault(g => (g.GroupId == groupId));
            if (group == null )
            {
                throw new Exception("Group not found from Id"); 
            }
            else
            {
                return group;
            }
        } 
        public Group CheckGroupManager(string groupId, string userId)
        {
            Group group = _repo.GetAllGroups().FirstOrDefault(g => (g.GroupId == groupId));
            if (group.GroupManagerId != userId )
            {
                throw new Exception("User does not have manager perms to perform this action"); 
            }
            else
            {
                return group;
            }
        }
        public Group UpdateGroupInformation(Group g_group, string userId)
        {
            try
            {
                CheckGroupId(g_group.GroupId);
                CheckGroupManager(g_group.GroupId, userId);
                return(_repo.UpdateGroup(g_group, userId));
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        //
        public Group DeleteGroup(string groupId, string managerId)
        {
            try
            {
                return(_repo.DeleteGroup(groupId, managerId));
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        } 
    }
}