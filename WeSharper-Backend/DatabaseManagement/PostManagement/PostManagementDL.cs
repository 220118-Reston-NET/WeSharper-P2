using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class PostManagementDL : IPostManagementDL
    {
        private readonly WeSharperContext _context;

        public PostManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Post AddNewPost(Post p_post)
        {
            _context.Posts.Add(p_post);
            _context.SaveChanges();

            return p_post;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public Post UpdatePost(Post p_post)
        {
            Post postToUpdate = _context.Posts.FirstOrDefault(p => p.UserId == p_post.UserId);
            if (postToUpdate != null)
            {
                postToUpdate.PostContent = p_post.PostContent;
                postToUpdate.PostPhoto = p_post.PostPhoto;
                postToUpdate.IsDeleted = p_post.IsDeleted;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No profiles found");
            }
            return p_post;
        }

        public Post DeletePost(Post p_post)
        {
            Post postToRemove = _context.Posts.FirstOrDefault(p => p.UserId == p_post.UserId);
            if (postToRemove != null)
            {
                _context.Posts.Remove(postToRemove);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Profile not found. Post could not be deleted.");
            }
            return p_post;
        }
    }
}