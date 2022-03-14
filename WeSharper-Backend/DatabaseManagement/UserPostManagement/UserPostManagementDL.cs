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

        public async Task<Post> AddNewUserPost(Post p_post)
        {
            await _context.AddAsync(p_post);
            await _context.SaveChangesAsync();

            return p_post;
        }

        public async Task<PostComment> AddNewUserPostComment(PostComment p_userPostComment)
        {
            await _context.AddAsync(p_userPostComment);
            await _context.SaveChangesAsync();

            return p_userPostComment;
        }

        public async Task<Post> DeleteUserPost(Post p_post)
        {
            Post _deletedPost = await _context.Posts.FirstOrDefaultAsync(p => p.PostId.Equals(p_post.PostId));
            if (_deletedPost != null)
            {
                _deletedPost.IsDeleted = true;
                _context.SaveChanges();
            }

            return _deletedPost;
        }

        public async Task<PostComment> DeleteUserPostComment(PostComment p_deletedPostComment)
        {
            PostComment _deletedPostComment = await _context.PostComments.FirstOrDefaultAsync(p => p.CommentId.Equals(p_deletedPostComment.CommentId));
            if (_deletedPostComment != null)
            {
                _deletedPostComment.IsDeleted = true;
                _context.SaveChanges();
            }

            return p_deletedPostComment;
        }

        public async Task<List<Post>> GetFeedsByUserID(string p_userID)
        {
            List<Post> _result = await (from p in _context.Posts
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
                                    .Where(p => p.IsDeleted.Equals(false))
                                    .Select(p => new Post
                                    {
                                        PostId = p.PostId,
                                        UserId = p.UserId,
                                        PostContent = p.PostContent,
                                        PostPhoto = p.PostPhoto,
                                        IsDeleted = p.IsDeleted,
                                        CreatedAt = p.CreatedAt,
                                        User = new ApplicationUser
                                        {
                                            UserName = p.User.UserName,
                                            Email = p.User.Email
                                        }
                                    })
                                    .OrderByDescending(p => p.CreatedAt)
                                    .ToListAsync();

            return _result;
        }

        public async Task<List<PostCommentReact>> GetPostCommentReactionsByCommentID(string p_postCommentID)
        {
            return await _context.PostCommentReacts.Where(p => p.CommentId.Equals(p_postCommentID)).ToListAsync();
        }

        public async Task<List<PostComment>> GetPostCommentsByPostID(string p_postID)
        {
            return await _context.PostComments.Where(p => p.PostId.Equals(p_postID)).ToListAsync();
        }

        public async Task<List<PostReact>> GetPostReactionsByPostID(string p_postID)
        {
            return await _context.PostReacts.Where(p => p.PostId.Equals(p_postID)).ToListAsync();
        }

        public async Task<List<Post>> GetUserPosts()
        {
            var _result = await _context.Posts
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
                                .Where(p => p.IsDeleted.Equals(false))
                                .OrderByDescending(p => p.CreatedAt)
                                .ToListAsync();

            return _result;
        }

        public async Task<PostReact> ReactUserPost(PostReact p_postReaction)
        {
            PostReact _postReact = await _context.PostReacts.FirstOrDefaultAsync(p => p.PostId.Equals(p_postReaction.PostId));
            if (_postReact != null)
            {
                _postReact.ReactId = p_postReaction.ReactId;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.AddAsync(p_postReaction);
                await _context.SaveChangesAsync();
            }

            return p_postReaction;
        }

        public async Task<PostCommentReact> ReactUserPostComment(PostCommentReact p_postCommentReaction)
        {
            PostCommentReact _postCommentReact = await _context.PostCommentReacts.FirstOrDefaultAsync(p => p.PostCommentReactId.Equals(p_postCommentReaction.PostCommentReactId));
            if (_postCommentReact != null)
            {
                _postCommentReact.ReactId = p_postCommentReaction.ReactId;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.AddAsync(p_postCommentReaction);
                await _context.SaveChangesAsync();
            }

            return p_postCommentReaction;
        }

        public async Task<Post> UpdateUserPost(Post p_post)
        {
            Post _updatedPost = await _context.Posts.FirstOrDefaultAsync(p => p.PostId.Equals(p_post.PostId));
            if (_updatedPost != null)
            {
                _updatedPost.PostContent = p_post.PostContent;
                _updatedPost.PostPhoto = p_post.PostPhoto;
                await _context.SaveChangesAsync();
            }

            return _updatedPost;
        }

        public async Task<PostComment> UpdateUserPostComment(PostComment p_updatePostComment)
        {
            PostComment _updatedPostComment = await _context.PostComments.FirstOrDefaultAsync(p => p.CommentId.Equals(p_updatePostComment.CommentId));
            if (_updatedPostComment != null)
            {
                _updatedPostComment.PostComment1 = p_updatePostComment.PostComment1;
                await _context.SaveChangesAsync();
            }

            return p_updatePostComment;
        }
    }
}