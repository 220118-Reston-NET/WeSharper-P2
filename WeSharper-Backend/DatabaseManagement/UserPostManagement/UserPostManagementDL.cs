using Microsoft.EntityFrameworkCore;
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
            _context.Add(p_post);
            _context.SaveChanges();

            return p_post;
        }

        public PostComment AddNewUserPostComment(PostComment p_userPostComment)
        {
            _context.Add(p_userPostComment);
            _context.SaveChanges();

            return p_userPostComment;
        }

        public Post DeleteUserPost(Post p_post)
        {
            Post _deletedPost = _context.Posts.FirstOrDefault(p => p.PostId.Equals(p_post.PostId));
            if (_deletedPost != null)
            {
                _deletedPost.IsDeleted = true;
                _context.SaveChanges();
            }

            return _deletedPost;
        }

        public PostComment DeleteUserPostComment(PostComment p_deletedPostComment)
        {
            PostComment _deletedPostComment = _context.PostComments.FirstOrDefault(p => p.CommentId.Equals(p_deletedPostComment.CommentId));
            if (_deletedPostComment != null)
            {
                _deletedPostComment.IsDeleted = true;
                _context.SaveChanges();
            }

            return p_deletedPostComment;
        }

        public List<Post> GetFeedsByUserID(string p_userID)
        {
            List<Post> _result = (from p in _context.Posts
                                  join f in _context.Friends
                                  on p.UserId equals f.AcceptedUserId
                                  where f.RequestedUserId == p_userID
                                    && f.IsAccepted
                                  select p)
                                  .Union
                                  (from p in _context.Posts
                                   join f in _context.Friends
                                   on p.UserId equals f.RequestedUserId
                                   where f.AcceptedUserId == p_userID
                                      && f.IsAccepted
                                   select p)
                                    .ToList();

            return _result;
        }

        public List<PostCommentReact> GetPostCommentReactionsByCommentID(string p_postCommentID)
        {
            return _context.PostCommentReacts.Where(p => p.CommentId.Equals(p_postCommentID)).ToList();
        }

        public List<PostComment> GetPostCommentsByPostID(string p_postID)
        {
            return _context.PostComments.Where(p => p.PostId.Equals(p_postID)).ToList();
        }

        public List<PostReact> GetPostReactionsByPostID(string p_postID)
        {
            return _context.PostReacts.Where(p => p.PostId.Equals(p_postID)).ToList();
        }

        public List<Post> GetUserPosts()
        {
            var _result = _context.Posts
                                .Select(p => new Post
                                {
                                    PostId = p.PostId,
                                    UserId = p.UserId,
                                    PostContent = p.PostContent,
                                    PostPhoto = p.PostPhoto,
                                    IsDeleted = p.IsDeleted,
                                    CreatedAt = p.CreatedAt,
                                    PostComments = p.PostComments
                                                    .Select(pc => new PostComment
                                                    {
                                                        CommentId = pc.CommentId,
                                                        UserId = pc.UserId,
                                                        PostComment1 = pc.PostComment1,
                                                        IsDeleted = pc.IsDeleted,
                                                        CreatedAt = pc.CreatedAt,
                                                        PostCommentReacts = pc.PostCommentReacts
                                                                                .Select(pcr => new PostCommentReact
                                                                                {
                                                                                    PostCommentReactId = pcr.PostCommentReactId,
                                                                                    UserId = pcr.UserId,
                                                                                    ReactId = pcr.ReactId
                                                                                }).ToList()
                                                    }).ToList(),
                                    PostReacts = p.PostReacts
                                                    .Select(pr => new PostReact
                                                    {
                                                        PostReactId = pr.PostReactId,
                                                        UserId = pr.UserId,
                                                        ReactId = pr.ReactId
                                                    }).ToList()
                                })
                                .ToList();

            return _result;
        }

        public PostReact ReactUserPost(PostReact p_postReaction)
        {
            PostReact _postReact = _context.PostReacts.FirstOrDefault(p => p.PostId.Equals(p_postReaction.PostId));
            if (_postReact != null)
            {
                _postReact.ReactId = p_postReaction.ReactId;
                _context.SaveChanges();
            }
            else
            {
                _context.Add(p_postReaction);
                _context.SaveChanges();
            }

            return p_postReaction;
        }

        public PostCommentReact ReactUserPostComment(PostCommentReact p_postCommentReaction)
        {
            PostCommentReact _postCommentReact = _context.PostCommentReacts.FirstOrDefault(p => p.PostCommentReactId.Equals(p_postCommentReaction.PostCommentReactId));
            if (_postCommentReact != null)
            {
                _postCommentReact.ReactId = p_postCommentReaction.ReactId;
                _context.SaveChanges();
            }
            else
            {
                _context.Add(p_postCommentReaction);
                _context.SaveChanges();
            }

            return p_postCommentReaction;
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

        public PostComment UpdateUserPostComment(PostComment p_updatePostComment)
        {
            PostComment _updatedPostComment = _context.PostComments.FirstOrDefault(p => p.CommentId.Equals(p_updatePostComment.CommentId));
            if (_updatedPostComment != null)
            {
                _updatedPostComment.PostComment1 = p_updatePostComment.PostComment1;
                _context.SaveChanges();
            }

            return p_updatePostComment;
        }
    }
}