using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IUserPostManagementDL
    {
        List<Post> GetUserPosts();
        Post AddNewUserPost(Post p_post);
        Post UpdateUserPost(Post p_post);
        List<Post> GetFeedsByUserID(string p_userID);
    }
}