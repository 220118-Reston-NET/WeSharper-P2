using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;
using System.Threading.Tasks;

namespace WeSharper.Test;

public class UserPostManagementBLTest
{
    [Fact]
    public async Task Should_Get_All_User_Posts()
    {
        //Arrange
        Post post1 = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            User = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Post> _expectedListUserPost = new List<Post>();
        _expectedListUserPost.Add(post1);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserPosts()).ReturnsAsync(_expectedListUserPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<Post> _actualListUserPost = await _userPostBL.GetUserPosts();

        //Assert
        Assert.Same(_expectedListUserPost, _actualListUserPost);
    }

    [Fact]
    public async Task Should_Get_A_User_Post()
    {
        //Arrange
        Post _expectedUserPost = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Post> _expectedListUserPost = new List<Post>();
        _expectedListUserPost.Add(_expectedUserPost);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserPosts()).ReturnsAsync(_expectedListUserPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualUserPost = await _userPostBL.GetUserPost(_expectedUserPost.PostId);

        //Assert
        Assert.Same(_expectedUserPost, _actualUserPost);
    }

    [Fact]
    public async Task Should_Get_All_User_Posts_By_UserID()
    {
        //Arrange
        Post _post1 = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            PostComments = new HashSet<PostComment>(),
            PostReacts = new HashSet<PostReact>(){
                new PostReact(){
                    React = new Reaction(){
                        ReactId = Guid.NewGuid().ToString(),
                        ReactName = "Love",
                        ReactIcon = "LoveIcon"
                    }
                }
            },
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Post> _expectedListUserPost = new List<Post>();
        _expectedListUserPost.Add(_post1);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserPosts()).ReturnsAsync(_expectedListUserPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<Post> _actualListUserPost = await _userPostBL.GetUserPostsByUserID(_post1.UserId);

        //Assert
        Assert.Equal(_expectedListUserPost.Count, _actualListUserPost.Count);
    }

    [Fact]
    public async Task Should_Update_User_Post()
    {
        //Arrange
        Post _expectedUpdatedPost = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            PostComments = new HashSet<PostComment>(),
            PostReacts = new HashSet<PostReact>(),
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateUserPost(_expectedUpdatedPost)).ReturnsAsync(_expectedUpdatedPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualUpdatedPost = await _userPostBL.UpdateUserPost(_expectedUpdatedPost);

        //Assert
        Assert.Same(_expectedUpdatedPost, _actualUpdatedPost);
    }

    [Fact]
    public async Task Should_Delete_User_Post()
    {
        //Arrange
        Post _expectedDeletedPost = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            PostComments = new HashSet<PostComment>(),
            PostReacts = new HashSet<PostReact>(),
            IsDeleted = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.DeleteUserPost(_expectedDeletedPost)).ReturnsAsync(_expectedDeletedPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualUpdatedPost = await _userPostBL.DeleteUserPost(_expectedDeletedPost);

        //Assert
        Assert.Same(_expectedDeletedPost, _actualUpdatedPost);
    }

    [Fact]
    public async Task Should_Add_New_User_Post()
    {
        //Arrange
        Post _expectedNewPost = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            PostComments = new HashSet<PostComment>(),
            PostReacts = new HashSet<PostReact>(),
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewUserPost(_expectedNewPost)).ReturnsAsync(_expectedNewPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualNewPost = await _userPostBL.AddNewUserPost(_expectedNewPost);

        //Assert
        Assert.Same(_expectedNewPost, _actualNewPost);
    }

    [Fact]
    public async Task Should_Get_All_User_Feeds()
    {
        //Arrange
        Post post1 = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Post> _expectedListUserFeed = new List<Post>();
        _expectedListUserFeed.Add(post1);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetFeedsByUserID(post1.UserId)).ReturnsAsync(_expectedListUserFeed);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<Post> _actualListUserFeed = await _userPostBL.GetFeedsByUserID(post1.UserId);

        //Assert
        Assert.Same(_expectedListUserFeed, _actualListUserFeed);
    }

    [Fact]
    public async Task Should_Get_All_User_Post_Comments()
    {
        //Arrange
        PostComment postComment = new PostComment()
        {
            CommentId = Guid.NewGuid().ToString(),
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostComment1 = "Hello World!",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<PostComment> _expectedListUserPostComment = new List<PostComment>();
        _expectedListUserPostComment.Add(postComment);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetPostCommentsByPostID(postComment.PostId)).ReturnsAsync(_expectedListUserPostComment);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<PostComment> _actualListUserPostComment = await _userPostBL.GetPostCommentsByPostID(postComment.PostId);

        //Assert
        Assert.Same(_expectedListUserPostComment, _actualListUserPostComment);
    }

    [Fact]
    public async Task Should_Add_New_User_Post_Comment()
    {
        //Arrange
        PostComment _expectednewPostComment = new PostComment()
        {
            CommentId = Guid.NewGuid().ToString(),
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostComment1 = "Hello World!",
            IsDeleted = false,
            Post = new Post(),
            User = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewUserPostComment(_expectednewPostComment)).ReturnsAsync(_expectednewPostComment);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        PostComment _actualUserPostComment = await _userPostBL.AddNewUserPostComment(_expectednewPostComment);

        //Assert
        Assert.Same(_expectednewPostComment, _actualUserPostComment);
    }

    [Fact]
    public async Task Should_Update_User_Post_Comment()
    {
        //Arrange
        PostComment _expectedUserPostComment = new PostComment()
        {
            CommentId = Guid.NewGuid().ToString(),
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostComment1 = "Hello World!",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateUserPostComment(_expectedUserPostComment)).ReturnsAsync(_expectedUserPostComment);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        PostComment _actualUserPostComment = await _userPostBL.UpdateUserPostComment(_expectedUserPostComment);

        //Assert
        Assert.Same(_expectedUserPostComment, _actualUserPostComment);
    }

    [Fact]
    public async Task Should_Delete_User_Post_Comment()
    {
        //Arrange
        PostComment _expectedUserPostComment = new PostComment()
        {
            CommentId = Guid.NewGuid().ToString(),
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostComment1 = "Hello World!",
            IsDeleted = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.DeleteUserPostComment(_expectedUserPostComment)).ReturnsAsync(_expectedUserPostComment);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        PostComment _actualUserPostComment = await _userPostBL.DeleteUserPostComment(_expectedUserPostComment);

        //Assert
        Assert.Same(_expectedUserPostComment, _actualUserPostComment);
    }

    [Fact]
    public async Task Should_Get_All_User_Post_React()
    {
        //Arrange
        PostReact _react1 = new PostReact()
        {
            PostReactId = Guid.NewGuid().ToString(),
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            ReactId = Guid.NewGuid().ToString()
        };
        List<PostReact> _expectedListUserPostReact = new List<PostReact>();
        _expectedListUserPostReact.Add(_react1);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetPostReactionsByPostID(_react1.PostId)).ReturnsAsync(_expectedListUserPostReact);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<PostReact> _actualListUserPostReact = await _userPostBL.GetPostReactionsByPostID(_react1.PostId);

        //Assert
        Assert.Same(_expectedListUserPostReact, _actualListUserPostReact);
    }

    [Fact]
    public async Task Should_React_To_User_Post()
    {
        //Arrange
        PostReact _expectedUserPostReact = new PostReact()
        {
            PostReactId = Guid.NewGuid().ToString(),
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            ReactId = Guid.NewGuid().ToString(),
            Post = new Post(),
            React = new Reaction(),
            User = new ApplicationUser()
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.ReactUserPost(_expectedUserPostReact)).ReturnsAsync(_expectedUserPostReact);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        PostReact _actualListUserPostReact = await _userPostBL.ReactUserPost(_expectedUserPostReact);

        //Assert
        Assert.Same(_expectedUserPostReact, _actualListUserPostReact);
    }

    [Fact]
    public async Task Should_Get_All_User_Post_Comment_React()
    {
        //Arrange
        PostCommentReact _commentReact = new PostCommentReact()
        {
            PostCommentReactId = Guid.NewGuid().ToString(),
            CommentId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            ReactId = Guid.NewGuid().ToString(),
            Comment = new PostComment(),
            React = new Reaction(),
            User = new ApplicationUser()
        };
        List<PostCommentReact> _expectedListUserPostCommentReact = new List<PostCommentReact>();
        _expectedListUserPostCommentReact.Add(_commentReact);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetPostCommentReactionsByCommentID(_commentReact.CommentId)).ReturnsAsync(_expectedListUserPostCommentReact);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<PostCommentReact> _actualListUserPostReact = await _userPostBL.GetPostCommentReactionsByCommentID(_commentReact.CommentId);

        //Assert
        Assert.Same(_expectedListUserPostCommentReact, _actualListUserPostReact);
    }

    [Fact]
    public async Task Should_React_To_User_Post_Comment()
    {
        //Arrange
        PostCommentReact _expectedUserPostCommentReact = new PostCommentReact()
        {
            PostCommentReactId = Guid.NewGuid().ToString(),
            CommentId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            ReactId = Guid.NewGuid().ToString()
        };

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.ReactUserPostComment(_expectedUserPostCommentReact)).ReturnsAsync(_expectedUserPostCommentReact);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        PostCommentReact _actualListUserPostReact = await _userPostBL.ReactUserPostComment(_expectedUserPostCommentReact);

        //Assert
        Assert.Same(_expectedUserPostCommentReact, _actualListUserPostReact);
    }
}