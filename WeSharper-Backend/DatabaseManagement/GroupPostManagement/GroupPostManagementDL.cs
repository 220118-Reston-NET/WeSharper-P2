using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class GroupPostManagementDL : IGroupPostManagementDL
    {
        private readonly WeSharperContext _context;

        public GroupPostManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public async Task<GroupPostComment> AddNewGroupPostComment(GroupPostComment p_groupPostComment)
        {
            await _context.AddAsync(p_groupPostComment);
            await _context.SaveChangesAsync();

            return p_groupPostComment;
        }

        public async Task<GroupPost> DeleteGroupPost(GroupPost p_groupPost)
        {
            var _deletedPost = await _context.GroupPosts.FirstOrDefaultAsync(p => p.GroupPostId.Equals(p_groupPost.GroupPostId));
            if (_deletedPost != null)
            {
                _deletedPost.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return _deletedPost;
        }

        public async Task<GroupPostComment> DeleteGroupPostComment(GroupPostComment p_groupPostComment)
        {
            var _deletedPostComment = await _context.GroupPostComments.FirstOrDefaultAsync(p => p.GroupPostCommentId.Equals(p_groupPostComment.GroupPostCommentId));
            if (_deletedPostComment != null)
            {
                _deletedPostComment.IsDeleted = true;
                await _context.SaveChangesAsync();
            }

            return _deletedPostComment;
        }

        public async Task<List<GroupPost>> GetGroupPosts()
        {
            var _result = await _context.GroupPosts
                                .Select(p => new GroupPost
                                {
                                    GroupPostId = p.GroupPostId,
                                    UserId = p.UserId,
                                    PostContent = p.PostContent,
                                    PostPhoto = p.PostPhoto,
                                    IsDeleted = p.IsDeleted,
                                    CreatedAt = p.CreatedAt,
                                    GroupPostComments = p.GroupPostComments
                                                    .Select(pc => new GroupPostComment
                                                    {
                                                        GroupPostCommentId = pc.GroupPostCommentId,
                                                        UserId = pc.UserId,
                                                        GroupPostComment1 = pc.GroupPostComment1,
                                                        IsDeleted = pc.IsDeleted,
                                                        CreatedAt = pc.CreatedAt,
                                                        GroupPostCommentReacts = pc.GroupPostCommentReacts
                                                                                .Select(pcr => new GroupPostCommentReact
                                                                                {
                                                                                    GroupPostCommentReactId = pcr.GroupPostCommentReactId,
                                                                                    UserId = pcr.UserId,
                                                                                    ReactId = pcr.ReactId
                                                                                }).ToList()
                                                    }).ToList(),
                                    GroupPostReacts = p.GroupPostReacts
                                                    .Select(pr => new GroupPostReact
                                                    {
                                                        GroupPostReactId = pr.GroupPostReactId,
                                                        UserId = pr.UserId,
                                                        ReactId = pr.ReactId
                                                    }).ToList()
                                })
                                .ToListAsync();

            return _result;
        }

        public async Task<GroupPost> PostNewPostToGroup(GroupPost p_groupPost)
        {
            _context.Add(p_groupPost);
            _context.SaveChanges();

            return p_groupPost;
        }

        public async Task<GroupPostReact> ReactGroupPost(GroupPostReact p_groupPostReact)
        {
            GroupPostReact _postReact = await _context.GroupPostReacts.FirstOrDefaultAsync(p => p.GroupPostId.Equals(p_groupPostReact.GroupPostId));
            if (_postReact != null)
            {
                _postReact.ReactId = p_groupPostReact.ReactId;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.AddAsync(p_groupPostReact);
                await _context.SaveChangesAsync();
            }

            return p_groupPostReact;
        }

        public async Task<GroupPostCommentReact> ReactGroupPostComment(GroupPostCommentReact p_groupPostCommentReact)
        {
            GroupPostCommentReact _postReact = await _context.GroupPostCommentReacts.FirstOrDefaultAsync(p => p.GroupPostCommentId.Equals(p_groupPostCommentReact.GroupPostCommentId));
            if (_postReact != null)
            {
                _postReact.ReactId = p_groupPostCommentReact.ReactId;
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.AddAsync(p_groupPostCommentReact);
                await _context.SaveChangesAsync();
            }

            return p_groupPostCommentReact;
        }

        public async Task<GroupPost> UpdateGroupPost(GroupPost p_groupPost)
        {
            var _updatedPost = await _context.GroupPosts.FirstOrDefaultAsync(p => p.GroupPostId.Equals(p_groupPost.GroupPostId));
            if (_updatedPost != null)
            {
                _updatedPost.PostContent = p_groupPost.PostContent;
                _updatedPost.PostPhoto = p_groupPost.PostPhoto;
                await _context.SaveChangesAsync();
            }

            return p_groupPost;
        }

        public async Task<GroupPostComment> UpdateGroupPostComment(GroupPostComment p_groupPostComment)
        {
            var _updatedPostComment = await _context.GroupPostComments.FirstOrDefaultAsync(p => p.GroupPostCommentId.Equals(p_groupPostComment.GroupPostCommentId));
            if (_updatedPostComment != null)
            {
                _updatedPostComment.GroupPostComment1 = p_groupPostComment.GroupPostComment1;
                await _context.SaveChangesAsync();
            }

            return _updatedPostComment;
        }
    }
}