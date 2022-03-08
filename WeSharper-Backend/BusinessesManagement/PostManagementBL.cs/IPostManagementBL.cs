using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IPostManagementBL
    {
        /// <summary>
        /// Adds a post to the database
        /// </summary>
        /// <param name="p_post"></param>
        /// <returns></returns>
        Post AddNewPost(Post p_post);
        /// <summary>
        /// Gets all of the posts from the database
        /// </summary>
        /// <returns></returns>
        List<Post> GetAllPosts();
        /// <summary>
        /// Gets a post from the database
        /// </summary>
        /// <returns></returns>
        Post GetAPost(string userId);
        /// <summary>
        /// Updates the information of the post in the database
        /// </summary>
        /// <param name="p_post"></param>
        /// <returns></returns>
        Post UpdatePost(Post p_post);
    }
}
