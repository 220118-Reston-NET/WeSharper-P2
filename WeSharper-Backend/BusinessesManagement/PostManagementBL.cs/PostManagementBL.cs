/*
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class PostManagementBL : IPostManagementBL
    {
        private readonly IPostManagementDL _repo;

        public PostManagementBL(IPostManagementDL repo)
        {
            _repo = repo;
        }

        public Post AddNewPost(Post p_post)
        {
            return _repo.AddNewPost(p_post);
        }
        public List<Post> GetAllPosts()
        {
            return _repo.GetAllPosts();
        }
        public Post GetAPost(string userId)
        {
            Post posts = _repo.GetAllPosts().FirstOrDefault(p => p.UserId == userId);
            if (posts != null)
            {
                return posts;
            }
            else
            {
                throw new Exception("Post not found");
            }
        }
        public Post UpdatePost(Post p_post)
        {
            return _repo.UpdatePost(p_post);
        }
    }
}


*/