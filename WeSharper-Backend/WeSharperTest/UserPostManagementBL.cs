using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;

namespace WeSharper.Test;

public class UserPostManagementBLTest
{
    [Fact]
    public void Should_Get_All_User_Posts()
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
        List<Post> _expectedListUserPost = new List<Post>();
        _expectedListUserPost.Add(post1);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserPosts()).Returns(_expectedListUserPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<Post> _actualListUserPost = _userPostBL.GetUserPosts();

        //Assert
        Assert.Same(_expectedListUserPost, _actualListUserPost);
    }

    [Fact]
    public void Should_Get_A_User_Post()
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
        _mockRepo.Setup(repo => repo.GetUserPosts()).Returns(_expectedListUserPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualUserPost = _userPostBL.GetUserPost(_expectedUserPost.PostId);

        //Assert
        Assert.Same(_expectedUserPost, _actualUserPost);
    }

    [Fact]
    public void Should_Get_All_User_Posts_By_UserID()
    {
        //Arrange
        Post _post1 = new Post()
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
        List<Post> _expectedListUserPost = new List<Post>();
        _expectedListUserPost.Add(_post1);

        Mock<IUserPostManagementDL> _mockRepo = new Mock<IUserPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetUserPosts()).Returns(_expectedListUserPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        List<Post> _actualListUserPost = _userPostBL.GetUserPostsByUserID(_post1.UserId);

        //Assert
        Assert.Equal(_expectedListUserPost.Count, _actualListUserPost.Count);
    }

    [Fact]
    public void Should_Update_User_Post()
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
        _mockRepo.Setup(repo => repo.UpdateUserPost(_expectedUpdatedPost)).Returns(_expectedUpdatedPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualUpdatedPost = _userPostBL.UpdateUserPost(_expectedUpdatedPost);

        //Assert
        Assert.Same(_expectedUpdatedPost, _actualUpdatedPost);
    }

    [Fact]
    public void Should_Add_New_User_Post()
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
        _mockRepo.Setup(repo => repo.AddNewUserPost(_expectedNewPost)).Returns(_expectedNewPost);
        IUserPostManagementBL _userPostBL = new UserPostManagementBL(_mockRepo.Object);

        //Act
        Post _actualNewPost = _userPostBL.AddNewUserPost(_expectedNewPost);

        //Assert
        Assert.Same(_expectedNewPost, _actualNewPost);
    }
}