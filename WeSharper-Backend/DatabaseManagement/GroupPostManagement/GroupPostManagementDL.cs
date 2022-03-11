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

        public GroupPostComment AddNewGroupPostComment(GroupPostComment p_groupPostComment)
        {
            _context.Add(p_groupPostComment);
            _context.SaveChanges();

            return p_groupPostComment;
        }

        public GroupPost DeleteGroupPost(GroupPost p_groupPost)
        {
            var _deletedPost = _context.GroupPosts.FirstOrDefault(p => p.GroupPostId.Equals(p_groupPost.GroupPostId));
            if (_deletedPost != null)
            {
                _deletedPost.IsDeleted = true;
                _context.SaveChanges();
            }

            return _deletedPost;
        }

        public GroupPostComment DeleteGroupPostComment(GroupPostComment p_groupPostComment)
        {
            var _deletedPostComment = _context.GroupPostComments.FirstOrDefault(p => p.GroupPostCommentId.Equals(p_groupPostComment.GroupPostCommentId));
            if (_deletedPostComment != null)
            {
                _deletedPostComment.IsDeleted = true;
                _context.SaveChanges();
            }

            return _deletedPostComment;
        }

        public GroupPost GetAllGroupPostByGroupPostID(string p_groupPostID)
        {
            var _result = _context.GroupPosts
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
                                .FirstOrDefault(p => p.GroupPostId.Equals(p_groupPostID));

            return _result;
        }

        public List<GroupPost> GetGroupPostsByGroupID(string p_groupID)
        {
            var _result = _context.GroupPosts
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
                                .Where(p => p.GroupId.Equals(p_groupID))
                                .ToList();

            return _result;
        }

        public GroupPost PostNewPostToGroup(GroupPost p_groupPost)
        {
            _context.Add(p_groupPost);
            _context.SaveChanges();

            return p_groupPost;
        }

        public GroupPostReact ReactGroupPost(GroupPostReact p_groupPostReact)
        {
            GroupPostReact _postReact = _context.GroupPostReacts.FirstOrDefault(p => p.GroupPostId.Equals(p_groupPostReact.GroupPostId));
            if (_postReact != null)
            {
                _postReact.ReactId = p_groupPostReact.ReactId;
                _context.SaveChanges();
            }
            else
            {
                _context.Add(p_groupPostReact);
                _context.SaveChanges();
            }

            return p_groupPostReact;
        }

        public GroupPostCommentReact ReactGroupPostComment(GroupPostCommentReact p_groupPostCommentReact)
        {
            GroupPostCommentReact _postReact = _context.GroupPostCommentReacts.FirstOrDefault(p => p.GroupPostCommentId.Equals(p_groupPostCommentReact.GroupPostCommentId));
            if (_postReact != null)
            {
                _postReact.ReactId = p_groupPostCommentReact.ReactId;
                _context.SaveChanges();
            }
            else
            {
                _context.Add(p_groupPostCommentReact);
                _context.SaveChanges();
            }

            return p_groupPostCommentReact;
        }

        public GroupPost UpdateGroupPost(GroupPost p_groupPost)
        {
            var _updatedPost = _context.GroupPosts.FirstOrDefault(p => p.GroupPostId.Equals(p_groupPost.GroupPostId));
            if (_updatedPost != null)
            {
                _updatedPost.PostContent = p_groupPost.PostContent;
                _updatedPost.PostPhoto = p_groupPost.PostPhoto;
                _context.SaveChanges();
            }

            return p_groupPost;
        }

        public GroupPostComment UpdateGroupPostComment(GroupPostComment p_groupPostComment)
        {
            var _updatedPostComment = _context.GroupPostComments.FirstOrDefault(p => p.GroupPostCommentId.Equals(p_groupPostComment.GroupPostCommentId));
            if (_updatedPostComment != null)
            {
                _updatedPostComment.GroupPostComment1 = p_groupPostComment.GroupPostComment1;
                _context.SaveChanges();
            }

            return _updatedPostComment;
        }
    }
}