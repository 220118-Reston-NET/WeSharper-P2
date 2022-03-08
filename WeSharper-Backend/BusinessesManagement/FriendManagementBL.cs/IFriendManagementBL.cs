using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IFriendManagementBL
    {
        Friend SendFriendRequest(Friend f_friend);
        List<Friend> GetAllFriends();
        Friend UpdateFriendRequest(Friend f_friend);
        Friend DeleteFriend(Friend f_friend);
        //bool ValidFriendName(string f_friend);
        bool CheckDuplicateRequest(string friendName);
    }
}