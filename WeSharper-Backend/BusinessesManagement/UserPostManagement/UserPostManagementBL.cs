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

        public Post AddNewUserPost(Post p_post)
        {
            p_post.PostId = Guid.NewGuid().ToString();
            p_post.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_post.IsDeleted = false;

            return _repo.AddNewUserPost(p_post);
        }

        public PostComment AddNewUserPostComment(PostComment p_userPostComment)
        {
            p_userPostComment.CommentId = Guid.NewGuid().ToString();
            p_userPostComment.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            p_userPostComment.IsDeleted = false;

            return _repo.AddNewUserPostComment(p_userPostComment);
        }

        public Post DeleteUserPost(Post p_post)
        {
            return _repo.DeleteUserPost(p_post);
        }

        public PostComment DeleteUserPostComment(PostComment p_deletedPostComment)
        {
            return _repo.DeleteUserPostComment(p_deletedPostComment);
        }

        public List<Post> GetFeedsByUserID(string p_userID)
        {
            return _repo.GetFeedsByUserID(p_userID);
        }

        public List<PostCommentReact> GetPostCommentReactionsByCommentID(string p_postCommentID)
        {
            return _repo.GetPostCommentReactionsByCommentID(p_postCommentID);
        }

        public List<PostComment> GetPostCommentsByPostID(string p_postID)
        {
            return _repo.GetPostCommentsByPostID(p_postID);
        }

        public List<PostReact> GetPostReactionsByPostID(string p_postID)
        {
            return _repo.GetPostReactionsByPostID(p_postID);
        }

        public Post GetUserPost(string p_postID)
        {
            return GetUserPosts().Find(p => p.PostId.Equals(p_postID));
        }

        public List<Post> GetUserPosts()
        {
            return _repo.GetUserPosts();
        }

        public List<Post> GetUserPostsByUserID(string p_userID)
        {
            return GetUserPosts().FindAll(p => p.UserId.Equals(p_userID));
        }

        public PostReact ReactUserPost(PostReact p_postReaction)
        {
            p_postReaction.PostReactId = Guid.NewGuid().ToString();
            return _repo.ReactUserPost(p_postReaction);
        }

        public PostCommentReact ReactUserPostComment(PostCommentReact p_postCommentReaction)
        {
            p_postCommentReaction.PostCommentReactId = Guid.NewGuid().ToString();
            return _repo.ReactUserPostComment(p_postCommentReaction);
        }

        public Post UpdateUserPost(Post p_post)
        {
            return _repo.UpdateUserPost(p_post);
        }

        public PostComment UpdateUserPostComment(PostComment p_updatePostComment)
        {
            return _repo.UpdateUserPostComment(p_updatePostComment);
        }
    }
}