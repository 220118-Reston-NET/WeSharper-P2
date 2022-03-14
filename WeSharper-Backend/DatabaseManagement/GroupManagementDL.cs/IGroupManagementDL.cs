using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IGroupManagementDL
    {
        /// <summary>
        /// Adds the Group into the database
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Task<Group> AddGroup(Group g_group);
        /// <summary>
        /// Retrieves all of the Groups in a list format
        /// </summary>
        /// <returns></returns>
        Task<List<Group>> GetAllGroups();
        /// <summary>
        /// Updates the information of the Group in the database
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Task<Group> UpdateGroup(Group g_group, string userId);
        /// <summary>
        /// Deletes a Group in the database. Only manager can disable group
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Task<Group> DeleteGroup(string groupId, string groupManagerId);

        // group user

        /// <summary>
        /// Sends a request for a user to join a group
        /// </summary>
        /// <param name="g_groupUser"></param>
        /// <returns></returns>

        Task<GroupUser> SendNewGroupUserRequest(GroupUser g_groupUser);
        Task<List<GroupUser>> GetAllGroupUsers();
        Task<List<GroupUser>> GetGroupApprovedUsersInGroup(string groupId);
        Task<GroupUser> UpdateGroupUser(GroupUser g_groupUser);
        Task<GroupUser> BanGroupUser(GroupUser g_groupUser);
        Task<GroupUser> UnbanGroupUser(GroupUser g_groupUser);
        Task<GroupUser> DeleteGroupUser(GroupUser g_groupUser);
    }
}