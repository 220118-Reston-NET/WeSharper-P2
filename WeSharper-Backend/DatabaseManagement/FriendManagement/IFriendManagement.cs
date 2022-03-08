using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IFriendManagementDL
    {
        /// <summary>
        /// Adds the friend into the database
        /// </summary>
        /// <param name="f_friend"></param>
        /// <returns></returns>
        Friend AddFriend(Friend f_friend);
        /// <summary>
        /// Retrieves all of the friends in a list format
        /// </summary>
        /// <returns></returns>
        List<Friend> GetAllFriends(); 
        /// <summary>
        /// Updates the information of the friend in the database
        /// </summary>
        /// <param name="f_friend"></param>
        /// <returns></returns>
        Friend UpdateFriend(Friend f_friend);
        /// <summary>
        /// Deletes a friend in the database
        /// </summary>
        /// <param name="f_friend"></param>
        /// <returns></returns>
        Friend DeleteFriend(Friend f_friend);
    }
}