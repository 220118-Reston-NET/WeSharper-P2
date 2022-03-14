using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IGroupPostManagementDL
    {
        Task<List<GroupPost>> GetGroupPosts();
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