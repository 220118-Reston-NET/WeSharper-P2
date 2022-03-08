using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class UserPostManagementDL : IUserPostManagementDL
    {
        private readonly WeSharperContext _context;

        public UserPostManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Post AddNewUserPost(Post p_post)
        {
            _context.Posts.Add(p_post);
            _context.SaveChanges();

            return p_post;
        }

        public List<Post> GetUserPosts()
        {
            return _context.Posts.ToList();
        }

        public Post UpdateUserPost(Post p_post)
        {
            Post _updatedPost = _context.Posts.FirstOrDefault(p => p.PostId.Equals(p_post.PostId));
            if (_updatedPost != null)
            {
                _updatedPost.PostContent = p_post.PostContent;
                _updatedPost.PostPhoto = p_post.PostPhoto;
                _context.SaveChanges();
            }

            return _updatedPost;
        }
    }
}