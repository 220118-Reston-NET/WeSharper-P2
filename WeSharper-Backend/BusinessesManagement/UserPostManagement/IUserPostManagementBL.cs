using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IUserPostManagementBL
    {
        List<Post> GetUserPosts();
        List<Post> GetUserPostsByUserID(string p_userID);
        Post GetUserPost(string p_postID);
        Post AddNewUserPost(Post p_post);
        Post UpdateUserPost(Post p_post);
        List<Post> GetFeedsByUserID(string p_userID);
    }
}