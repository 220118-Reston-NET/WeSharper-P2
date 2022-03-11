using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IGroupPostManagementDL
    {
        List<GroupPost> GetGroupPosts();
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