using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IPostManagementDL
    {
        /// <summary>
        /// Adds the post into the database
        /// </summary>
        /// <param name="p_post"></param>
        /// <returns></returns>
        Post AddNewPost(Post p_post);
        /// <summary>
        /// Retrieves all of the hobbies in a list format
        /// </summary>
        /// <returns></returns>
        List<Post> GetAllPosts(); 
        /// <summary>
        /// Updates the information of the post in the database
        /// </summary>
        /// <param name="p_post"></param>
        /// <returns></returns>
        Post UpdatePost(Post p_post);
        /// <summary>
        /// Deletes a post in the database
        /// </summary>
        /// <param name="p_post"></param>
        /// <returns></returns>
        Post DeletePost(Post p_post);
    }
}