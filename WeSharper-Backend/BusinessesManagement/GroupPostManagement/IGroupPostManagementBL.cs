using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IGroupPostManagementBL
    {
        Task<List<GroupPost>> GetGroupPosts();
        Task<List<GroupPost>> GetGroupPostsByGroupID(string p_groupID);
        Task<GroupPost> GetAllGroupPostByGroupPostID(string p_groupPostID);
        Task<GroupPost> PostNewPostToGroup(GroupPost p_groupPost);
        Task<GroupPost> UpdateGroupPost(GroupPost p_groupPost);
        Task<GroupPost> DeleteGroupPost(GroupPost p_groupPost);
        Task<GroupPostComment> AddNewGroupPostComment(GroupPostComment p_groupPostComment);
        Task<GroupPostComment> UpdateGroupPostComment(GroupPostComment p_groupPostComment);
        Task<GroupPostComment> DeleteGroupPostComment(GroupPostComment p_groupPostComment);
        Task<GroupPostReact> ReactGroupPost(GroupPostReact p_groupPostReact);
        Task<GroupPostCommentReact> ReactGroupPostComment(GroupPostCommentReact p_groupPostCommentReact);
    }
}