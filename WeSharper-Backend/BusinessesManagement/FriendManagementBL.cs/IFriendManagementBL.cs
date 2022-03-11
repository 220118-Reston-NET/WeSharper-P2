using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IFriendManagementBL
    {
        /// <summary>
        /// creates a new friend request
        /// </summary>
        /// <param name="f_friend"></param>
        /// <returns></returns>
        Friend SendFriendRequest(Friend f_friend);
        /// <summary>
        /// returns the list of ALL of the friends
        /// </summary>
        /// <returns></returns>
        List<Friend> GetAllFriends();
        /// <summary>
        /// gets a list of friends for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Friend> GetUserFriends(string userId);
        /// <summary>
        /// updates the friend status
        /// </summary>
        /// <param name="f_friend"></param>
        /// <returns></returns>
        Friend UpdateFriendRequest(Friend f_friend);
        /// <summary>
        /// deletes the friend
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        Friend DeleteFriend(string userId, string friendId);
        //bool ValidFriendName(string f_friend);
        /// <summary>
        /// checks to see if the user is already friends/has sent a request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        Friend CheckFriend(string userId, string friendId);
        List<Friend> GetUnacceptedSentRequests(string userId);
        List<Friend> GetUserPendingFriendRequests(string userId);
        
    }
}