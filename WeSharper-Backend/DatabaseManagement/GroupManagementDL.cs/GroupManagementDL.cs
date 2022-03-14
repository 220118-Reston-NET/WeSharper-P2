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

        public async Task<Group> AddGroup(Group g_group)
        {
            Group newGroup = new Group()
            {
                GroupId = Guid.NewGuid().ToString(),
                GroupManagerId = g_group.GroupManagerId,
                GroupName = g_group.GroupName,
                GroupPicture = g_group.GroupPicture,
                Description = g_group.Description,

            };
            await _context.Groups.AddAsync(newGroup);
            await _context.SaveChangesAsync();

            return newGroup;
        }

        public async Task<List<Group>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }


        public async Task<Group> UpdateGroup(Group g_group, string userId)
        {
            Group groupToUpdate = await _context.Groups.Where(g => g.GroupId == g_group.GroupId).FirstOrDefaultAsync();
            if (groupToUpdate != null)
            {
                if (groupToUpdate.GroupManagerId == userId)
                {
                    if (g_group.GroupManagerId != null)
                        groupToUpdate.GroupManagerId = g_group.GroupManagerId;
                    if (g_group.GroupName != null)
                        groupToUpdate.GroupName = g_group.GroupName;
                    groupToUpdate.GroupPicture = g_group.GroupPicture;
                    groupToUpdate.Description = g_group.Description;
                }
                else
                {
                    throw new Exception("User does not have perms to update");
                }

                await _context.SaveChangesAsync();
                return groupToUpdate;
            }
            else
            {
                throw new Exception("No groups found with that manager");
            }

        }

        public async Task<Group> DeleteGroup(string groupId, string groupManagerId)
        {
            Group groupToRemove = await _context.Groups.Where(g => (g.GroupId == groupId)).FirstOrDefaultAsync();
            if (groupToRemove != null)
            {
                if (groupToRemove.GroupManagerId == groupManagerId)
                {
                    groupToRemove.IsActivated = false;
                    await _context.SaveChangesAsync();
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




        public async Task<GroupUser> SendNewGroupUserRequest(GroupUser g_groupUser)
        {
            g_groupUser.User = await _context.Users.FirstOrDefaultAsync(u => (u.Id == g_groupUser.UserId));
            g_groupUser.Group = await _context.Groups.FirstOrDefaultAsync(g => (g.GroupId == g_groupUser.GroupId));
            await _context.GroupUsers.AddAsync(g_groupUser);
            await _context.SaveChangesAsync();
            return g_groupUser;
        }


        public async Task<List<GroupUser>> GetAllGroupUsers()
        {
            return await _context.GroupUsers.ToListAsync();
        }
        public async Task<List<GroupUser>> GetGroupApprovedUsersInGroup(string groupId)
        {
            return await _context.GroupUsers.Where(g => (g.GroupId == groupId) && (g.IsApproved == true) && (g.IsBanned == false)).ToListAsync();
        }

        public async Task<GroupUser> UpdateGroupUser(GroupUser g_groupUser)
        {
            try
            {
                GroupUser groupUserToUpdate = await _context.GroupUsers.FirstOrDefaultAsync(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));

                await _context.SaveChangesAsync();
                return groupUserToUpdate;
            }
            catch (System.Exception exe)
            {
                throw new Exception("User could not be found in the group");
            }
        }
        public async Task<GroupUser> BanGroupUser(GroupUser g_groupUser)
        {
            GroupUser groupUser = await _context.GroupUsers.FirstOrDefaultAsync(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));
            groupUser.IsBanned = true;
            await _context.SaveChangesAsync();
            return groupUser;
        }
        public async Task<GroupUser> UnbanGroupUser(GroupUser g_groupUser)
        {
            GroupUser groupUser = await _context.GroupUsers.FirstOrDefaultAsync(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));
            groupUser.IsBanned = false;
            await _context.SaveChangesAsync();
            return groupUser;
        }
        public async Task<GroupUser> DeleteGroupUser(GroupUser g_groupUser)
        {
            GroupUser groupUserToRemove = await _context.GroupUsers.FirstOrDefaultAsync(g => (g.UserId == g_groupUser.UserId) && (g.GroupId == g_groupUser.GroupId));
            Console.WriteLine("crymaru");
            _context.Remove(groupUserToRemove);
            return groupUserToRemove;
        }


    }
}
