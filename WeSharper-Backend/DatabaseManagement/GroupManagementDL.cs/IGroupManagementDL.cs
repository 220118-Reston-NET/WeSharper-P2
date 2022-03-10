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
        Group AddGroup(Group g_group);
        /// <summary>
        /// Retrieves all of the Groups in a list format
        /// </summary>
        /// <returns></returns>
        List<Group> GetAllGroups(); 
        /// <summary>
        /// Updates the information of the Group in the database
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Group UpdateGroup(Group g_group, string userId);
        /// <summary>
        /// Deletes a Group in the database. Only manager can disable group
        /// </summary>
        /// <param name="g_group"></param>
        /// <returns></returns>
        Group DeleteGroup(string groupId, string groupManagerId);
    }
}