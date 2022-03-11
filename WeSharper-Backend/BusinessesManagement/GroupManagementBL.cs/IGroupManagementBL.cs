using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IGroupManagementBL
    {
        /// <summary>
        /// creates a new group request
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Group AddNewGroup(Group g_group);
        /// <summary>
        /// returns the list of ALL of the groups
        /// </summary>
        /// <returns></returns>
        List<Group> GetAllGroups();
        /*
        /// <summary>
        /// gets group info from the group's name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Group GetGroupFromName(string groupName);
        */
        /// <summary>
        /// updates the group status
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Group UpdateGroupInformation(Group g_group, string userId);
        /// <summary>
        /// deletes the group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        Group DeleteGroup(string userId, string groupId);
        public Group CheckGroupManager(string groupId, string userId);
        public Group CheckGroupId(string groupId);


        // Group User
        //GroupUser SendNewGroupUserRequest(GroupUser g_groupUser);
        List<GroupUser> GetAllGroupUsers();
        List<GroupUser> GetApprovedUsersInGroup(string groupId);
        
        List<GroupUser> GetGroupUnapprovedJoinRequests(string groupId);
/*        GroupUser UpdateGroupUser(GroupUser g_groupUser);
        GroupUser BanGroupUser(GroupUser g_groupUser);
        GroupUser UnbanGroupUser(GroupUser g_groupUser);
        GroupUser DeleteGroupUser(GroupUser g_groupUser); */
        bool CheckValidGroupUser(string groupId, string userId); 
    }
}