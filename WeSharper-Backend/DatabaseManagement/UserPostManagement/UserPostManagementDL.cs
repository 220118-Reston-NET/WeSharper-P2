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

        public List<Post> GetFeedsByUserID(string p_userID)
        {
            return _context.Posts.Join(_context.Friends,
                                        p => p.UserId,
                                        f => f.AcceptedUserId,
                                        (p, f) => new Post()
                                        {
                                            UserId = p.UserId,
                                            PostId = p.PostId,
                                            PostContent = p.PostContent,
                                            PostPhoto = p.PostPhoto,
                                            PostComments = p.PostComments,
                                            PostReacts = p.PostReacts,
                                            User = p.User,
                                            CreatedAt = p.CreatedAt
                                        }).ToList();
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