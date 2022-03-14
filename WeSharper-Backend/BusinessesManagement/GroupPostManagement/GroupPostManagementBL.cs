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

        public async Task<GroupPostComment> AddNewGroupPostComment(GroupPostComment p_groupPostComment)
        {
            p_groupPostComment.GroupPostCommentId = Guid.NewGuid().ToString();
            p_groupPostComment.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_groupPostComment.IsDeleted = false;

            return await _repo.AddNewGroupPostComment(p_groupPostComment);
        }

        public async Task<GroupPost> DeleteGroupPost(GroupPost p_groupPost)
        {
            return await _repo.DeleteGroupPost(p_groupPost);
        }

        public async Task<GroupPostComment> DeleteGroupPostComment(GroupPostComment p_groupPostComment)
        {
            return await _repo.DeleteGroupPostComment(p_groupPostComment);
        }

        public async Task<GroupPost> GetAllGroupPostByGroupPostID(string p_groupPostID)
        {
            List<GroupPost> _result = await GetGroupPosts();
            return _result.FirstOrDefault(p => p.GroupPostId.Equals(p_groupPostID));
        }

        public async Task<List<GroupPost>> GetGroupPosts()
        {
            return await _repo.GetGroupPosts();
        }

        public async Task<List<GroupPost>> GetGroupPostsByGroupID(string p_groupID)
        {
            List<GroupPost> _result = await GetGroupPosts();
            return _result.FindAll(p => p.GroupId.Equals(p_groupID));
        }

        public async Task<GroupPost> PostNewPostToGroup(GroupPost p_groupPost)
        {
            p_groupPost.GroupPostId = Guid.NewGuid().ToString();
            p_groupPost.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_groupPost.IsDeleted = false;

            return await _repo.PostNewPostToGroup(p_groupPost);
        }

        public async Task<GroupPostReact> ReactGroupPost(GroupPostReact p_groupPostReact)
        {
            p_groupPostReact.GroupPostReactId = Guid.NewGuid().ToString();
            return await _repo.ReactGroupPost(p_groupPostReact);
        }

        public async Task<GroupPostCommentReact> ReactGroupPostComment(GroupPostCommentReact p_groupPostCommentReact)
        {
            p_groupPostCommentReact.GroupPostCommentReactId = Guid.NewGuid().ToString();
            return await _repo.ReactGroupPostComment(p_groupPostCommentReact);
        }

        public async Task<GroupPost> UpdateGroupPost(GroupPost p_groupPost)
        {
            return await _repo.UpdateGroupPost(p_groupPost);
        }

        public async Task<GroupPostComment> UpdateGroupPostComment(GroupPostComment p_groupPostComment)
        {
            return await _repo.UpdateGroupPostComment(p_groupPostComment);
        }
    }
}