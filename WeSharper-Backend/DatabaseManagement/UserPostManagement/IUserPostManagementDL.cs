using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IUserPostManagementDL
    {
        List<Post> GetUserPosts();
        Post AddNewUserPost(Post p_post);
        Post UpdateUserPost(Post p_post);
        List<Post> GetFeedsByUserID(string p_userID);
        List<PostComment> GetPostCommentsByPostID(string p_postID);
        PostComment AddNewUserPostComment(PostComment p_userPostComment);
        PostComment UpdateUserPostComment(PostComment p_updatePostComment);
        PostComment DeleteUserPostComment(PostComment p_deletedPostComment);
        List<PostReact> GetPostReactionsByPostID(string p_postID);
        PostReact ReactUserPost(PostReact p_postReaction);
        List<PostCommentReact> GetPostCommentReactionsByCommentID(string p_postCommentID);
        PostCommentReact ReactUserPostComment(PostCommentReact p_postCommentReaction);
    }
}