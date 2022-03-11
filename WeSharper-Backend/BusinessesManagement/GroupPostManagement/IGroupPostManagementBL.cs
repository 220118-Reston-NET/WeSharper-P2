using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IGroupPostManagementBL
    {
        List<GroupPost> GetGroupPosts();
        List<GroupPost> GetGroupPostsByGroupID(string p_groupID);
        GroupPost GetAllGroupPostByGroupPostID(string p_groupPostID);
        GroupPost PostNewPostToGroup(GroupPost p_groupPost);
        GroupPost UpdateGroupPost(GroupPost p_groupPost);
        GroupPost DeleteGroupPost(GroupPost p_groupPost);
        GroupPostComment AddNewGroupPostComment(GroupPostComment p_groupPostComment);
        GroupPostComment UpdateGroupPostComment(GroupPostComment p_groupPostComment);
        GroupPostComment DeleteGroupPostComment(GroupPostComment p_groupPostComment);
        GroupPostReact ReactGroupPost(GroupPostReact p_groupPostReact);
        GroupPostCommentReact ReactGroupPostComment(GroupPostCommentReact p_groupPostCommentReact);
    }
}