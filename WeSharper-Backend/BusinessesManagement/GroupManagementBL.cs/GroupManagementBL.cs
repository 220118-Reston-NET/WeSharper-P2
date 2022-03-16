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

        public async Task<Group> AddNewGroup(Group g_group)
        {
            return await _repo.AddGroup(g_group);
        }
        public async Task<List<Group>> GetAllGroups()
        {
            List<Group> allGroups = await _repo.GetAllGroups();
            if (!allGroups.Any())
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
        public async Task<Group> CheckGroupId(string groupId)
        {
            List<Group> _result = await _repo.GetAllGroups();
            Group group = _result.FirstOrDefault(g => (g.GroupId == groupId));
            if (group == null)
            {
                throw new Exception("Group not found from Id");
            }
            else
            {
                return group;
            }
        }
        public async Task<Group> CheckGroupManager(string groupId, string userId)
        {
            List<Group> _result = await _repo.GetAllGroups();
            Group group = _result.FirstOrDefault(g => (g.GroupId == groupId));
            if (group.GroupManagerId != userId)
            {
                throw new Exception("User does not have manager perms to perform this action");
            }
            else
            {
                return group;
            }
        }
        public async Task<Group> UpdateGroupInformation(Group g_group, string userId)
        {
            try
            {
                CheckGroupId(g_group.GroupId);
                CheckGroupManager(g_group.GroupId, userId);
                return await _repo.UpdateGroup(g_group, userId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<Group> DeleteGroup(string groupId, string managerId)
        {
            try
            {
                return await _repo.DeleteGroup(groupId, managerId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        // GroupUser
        /*        public GroupUser SendNewGroupUserRequest(GroupUser g_groupUser)
                {
                    g_groupUser.IsBanned = false;
                    g_groupUser.IsApproved = false;
                    return _repo.SendNewGroupUserRequest(g_groupUser);
                } */
        public async Task<List<GroupUser>> GetAllGroupUsers()
        {
            List<GroupUser> allGroupUsers = await _repo.GetAllGroupUsers();
            if (!allGroupUsers.Any())
            {
                throw new Exception("No one has any groups");
            }
            else
            {
                return allGroupUsers;
            }
        }
        public async Task<List<GroupUser>> GetApprovedUsersInGroup(string groupId)
        {
            List<GroupUser> acceptedUserInGroups = await _repo.GetGroupApprovedUsersInGroup(groupId);
            if (!acceptedUserInGroups.Any())
            {
                throw new Exception("No one has any groups");
            }
            else
            {
                return acceptedUserInGroups;
            }
        }
        public async Task<List<GroupUser>> GetGroupUnapprovedJoinRequests(string groupId)
        {
            try
            {
                List<GroupUser> _result = await GetAllGroupUsers();
                List<GroupUser> joinRequests = _result.Where(g => (g.GroupId == groupId) && (!g.IsApproved)).ToList();
                return joinRequests;
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        /*        public GroupUser UpdateGroupUser(GroupUser g_groupUser)
                {
                    try
                    {
                        CheckGroupId(g_groupUser.GroupId);
                        CheckValidGroupUser(g_groupUser.GroupId, g_groupUser.UserId);
                        return(_repo.UpdateGroupUser(g_groupUser));
                    }
                    catch(System.Exception exe)
                    {
                        throw new Exception(exe.Message);
                    } 
                }
                public GroupUser BanGroupUser(GroupUser g_groupUser)
                {
                    try
                    {
                        CheckGroupId(g_groupUser.GroupId);
                        CheckValidGroupUser(g_groupUser.GroupId, g_groupUser.UserId);
                        return(_repo.BanGroupUser(g_groupUser));
                    }
                    catch(System.Exception exe)
                    {
                        throw new Exception(exe.Message);
                    }
                }
                public GroupUser UnbanGroupUser(GroupUser g_groupUser)
                {
                    try
                    {
                        CheckGroupId(g_groupUser.GroupId);
                        CheckValidGroupUser(g_groupUser.GroupId, g_groupUser.UserId);
                        return(_repo.UnbanGroupUser(g_groupUser));
                    }
                    catch(System.Exception exe)
                    {
                        throw new Exception(exe.Message);
                    }
                }
                public GroupUser DeleteGroupUser(GroupUser g_groupUser)
                {
                    try
                    {
                        CheckGroupId(g_groupUser.GroupId);
                        CheckValidGroupUser(g_groupUser.GroupId, g_groupUser.UserId);
                        return(_repo.DeleteGroupUser(g_groupUser));
                    }
                    catch(System.Exception exe)
                    {
                        throw new Exception(exe.Message);
                    }
                } */
        /* public async Task<bool> CheckValidGroupUser(string groupId, string userId)
        {
            List<GroupUser> _result = await GetAllGroupUsers();
            GroupUser tempGroupUser = _result.FirstOrDefault(g => (g.UserId == userId) && (g.GroupId == groupId));
            if (tempGroupUser == null)
            {
                throw new Exception("Invalid user in group");
            }
            return true;
        } */

    }
}