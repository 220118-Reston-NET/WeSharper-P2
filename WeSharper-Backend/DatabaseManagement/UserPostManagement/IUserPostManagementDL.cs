using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IUserPostManagementDL
    {
        Task<List<Post>> GetUserPosts();
        Task<Post> AddNewUserPost(Post p_post);
        Task<Post> UpdateUserPost(Post p_post);
        Task<Post> DeleteUserPost(Post p_post);
        Task<List<Post>> GetFeedsByUserID(string p_userID);
        Task<List<PostComment>> GetPostCommentsByPostID(string p_postID);
        Task<PostComment> AddNewUserPostComment(PostComment p_userPostComment);
        Task<PostComment> UpdateUserPostComment(PostComment p_updatePostComment);
        Task<PostComment> DeleteUserPostComment(PostComment p_deletedPostComment);
        Task<List<PostReact>> GetPostReactionsByPostID(string p_postID);
        Task<PostReact> ReactUserPost(PostReact p_postReaction);
        Task<List<PostCommentReact>> GetPostCommentReactionsByCommentID(string p_postCommentID);
        Task<PostCommentReact> ReactUserPostComment(PostCommentReact p_postCommentReaction);
    }
}