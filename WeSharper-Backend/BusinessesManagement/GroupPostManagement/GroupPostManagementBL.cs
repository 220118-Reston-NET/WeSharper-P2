using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class GroupPostManagementBL : IGroupPostManagementBL
    {
        private readonly IGroupPostManagementDL _repo;

        public GroupPostManagementBL(IGroupPostManagementDL repo)
        {
            _repo = repo;
        }

        public GroupPostComment AddNewGroupPostComment(GroupPostComment p_groupPostComment)
        {
            p_groupPostComment.GroupPostCommentId = Guid.NewGuid().ToString();
            p_groupPostComment.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_groupPostComment.IsDeleted = false;

            return _repo.AddNewGroupPostComment(p_groupPostComment);
        }

        public GroupPost DeleteGroupPost(GroupPost p_groupPost)
        {
            return _repo.DeleteGroupPost(p_groupPost);
        }

        public GroupPostComment DeleteGroupPostComment(GroupPostComment p_groupPostComment)
        {
            return _repo.DeleteGroupPostComment(p_groupPostComment);
        }

        public GroupPost GetAllGroupPostByGroupPostID(string p_groupPostID)
        {
            return GetGroupPosts().FirstOrDefault(p => p.GroupPostId.Equals(p_groupPostID));
        }

        public List<GroupPost> GetGroupPosts()
        {
            return _repo.GetGroupPosts();
        }

        public List<GroupPost> GetGroupPostsByGroupID(string p_groupID)
        {
            return GetGroupPosts().FindAll(p => p.GroupId.Equals(p_groupID));
        }

        public GroupPost PostNewPostToGroup(GroupPost p_groupPost)
        {
            p_groupPost.GroupPostId = Guid.NewGuid().ToString();
            p_groupPost.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_groupPost.IsDeleted = false;

            return _repo.PostNewPostToGroup(p_groupPost);
        }

        public GroupPostReact ReactGroupPost(GroupPostReact p_groupPostReact)
        {
            p_groupPostReact.GroupPostReactId = Guid.NewGuid().ToString();
            return _repo.ReactGroupPost(p_groupPostReact);
        }

        public GroupPostCommentReact ReactGroupPostComment(GroupPostCommentReact p_groupPostCommentReact)
        {
            p_groupPostCommentReact.GroupPostCommentReactId = Guid.NewGuid().ToString();
            return _repo.ReactGroupPostComment(p_groupPostCommentReact);
        }

        public GroupPost UpdateGroupPost(GroupPost p_groupPost)
        {
            return _repo.UpdateGroupPost(p_groupPost);
        }

        public GroupPostComment UpdateGroupPostComment(GroupPostComment p_groupPostComment)
        {
            return _repo.UpdateGroupPostComment(p_groupPostComment);
        }
    }
}