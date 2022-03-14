using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class UserPostManagementBL : IUserPostManagementBL
    {
        private readonly IUserPostManagementDL _repo;

        public UserPostManagementBL(IUserPostManagementDL repo)
        {
            _repo = repo;
        }

        public async Task<Post> AddNewUserPost(Post p_post)
        {
            p_post.PostId = Guid.NewGuid().ToString();
            p_post.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_post.IsDeleted = false;

            return await _repo.AddNewUserPost(p_post);
        }

        public async Task<PostComment> AddNewUserPostComment(PostComment p_userPostComment)
        {
            p_userPostComment.CommentId = Guid.NewGuid().ToString();
            p_userPostComment.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_userPostComment.IsDeleted = false;

            return await _repo.AddNewUserPostComment(p_userPostComment);
        }

        public async Task<Post> DeleteUserPost(Post p_post)
        {
            return await _repo.DeleteUserPost(p_post);
        }

        public async Task<PostComment> DeleteUserPostComment(PostComment p_deletedPostComment)
        {
            return await _repo.DeleteUserPostComment(p_deletedPostComment);
        }

        public async Task<List<Post>> GetFeedsByUserID(string p_userID)
        {
            return await _repo.GetFeedsByUserID(p_userID);
        }

        public async Task<List<PostCommentReact>> GetPostCommentReactionsByCommentID(string p_postCommentID)
        {
            return await _repo.GetPostCommentReactionsByCommentID(p_postCommentID);
        }

        public async Task<List<PostComment>> GetPostCommentsByPostID(string p_postID)
        {
            return await _repo.GetPostCommentsByPostID(p_postID);
        }

        public async Task<List<PostReact>> GetPostReactionsByPostID(string p_postID)
        {
            return await _repo.GetPostReactionsByPostID(p_postID);
        }

        public async Task<Post> GetUserPost(string p_postID)
        {
            var _result = await GetUserPosts();
            return _result.Find(p => p.PostId.Equals(p_postID));
        }

        public async Task<List<Post>> GetUserPosts()
        {
            return await _repo.GetUserPosts();
        }

        public async Task<List<Post>> GetUserPostsByUserID(string p_userID)
        {
            var _result = await GetUserPosts();
            return _result.FindAll(p => p.UserId.Equals(p_userID));
        }

        public async Task<PostReact> ReactUserPost(PostReact p_postReaction)
        {
            p_postReaction.PostReactId = Guid.NewGuid().ToString();
            return await _repo.ReactUserPost(p_postReaction);
        }

        public async Task<PostCommentReact> ReactUserPostComment(PostCommentReact p_postCommentReaction)
        {
            p_postCommentReaction.PostCommentReactId = Guid.NewGuid().ToString();
            return await _repo.ReactUserPostComment(p_postCommentReaction);
        }

        public async Task<Post> UpdateUserPost(Post p_post)
        {
            return await _repo.UpdateUserPost(p_post);
        }

        public async Task<PostComment> UpdateUserPostComment(PostComment p_updatePostComment)
        {
            return await _repo.UpdateUserPostComment(p_updatePostComment);
        }
    }
}