using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class UserPostManagementBL : IUserPostManagementBL
    {
        private readonly IUserPostManagementDL _repo;

        public UserPostManagementBL(IUserPostManagementDL repo)
        {
            _repo = repo;
        }

        public Post AddNewUserPost(Post p_post)
        {
            p_post.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_post.IsDeleted = false;

            return _repo.AddNewUserPost(p_post);
        }

        public List<Post> GetFeedsByUserID(string p_userID)
        {
            return _repo.GetFeedsByUserID(p_userID);
        }

        public Post GetUserPost(string p_postID)
        {
            return GetUserPosts().Find(p => p.PostId.Equals(p_postID));
        }

        public List<Post> GetUserPosts()
        {
            return _repo.GetUserPosts();
        }

        public List<Post> GetUserPostsByUserID(string p_userID)
        {
            return GetUserPosts().FindAll(p => p.UserId.Equals(p_userID));
        }

        public Post UpdateUserPost(Post p_post)
        {
            return _repo.UpdateUserPost(p_post);
        }
    }
}