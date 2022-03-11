using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;

namespace WeSharper.Test;

public class GroupPostManagementBLTest
{
    [Fact]
    public void Should_Get_All_Group_Posts_By_GroupID()
    {
        //Arrange
        GroupPost _groupPost = new GroupPost()
        {
            GroupPostId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            User = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<GroupPost> _expectedListGroupPost = new List<GroupPost>();
        _expectedListGroupPost.Add(_groupPost);

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetGroupPostsByGroupID(_groupPost.GroupId)).Returns(_expectedListGroupPost);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        List<GroupPost> _actualListGroupPost = _groupPostBL.GetGroupPostsByGroupID(_groupPost.GroupId);

        //Assert
        Assert.Same(_expectedListGroupPost, _actualListGroupPost);
    }

    [Fact]
    public void Should_Get_Group_Posts_By_GroupPostID()
    {
        //Arrange
        GroupPost _expectedGroupPost = new GroupPost()
        {
            GroupPostId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroupPostByGroupPostID(_expectedGroupPost.GroupPostId)).Returns(_expectedGroupPost);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPost _actualGroupPost = _groupPostBL.GetAllGroupPostByGroupPostID(_expectedGroupPost.GroupPostId);

        //Assert
        Assert.Same(_expectedGroupPost, _actualGroupPost);
    }

    [Fact]
    public void Should_Post_New_Group_Post_To_Group()
    {
        //Arrange
        GroupPost _expectedGroupPost = new GroupPost()
        {
            GroupPostId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.PostNewPostToGroup(_expectedGroupPost)).Returns(_expectedGroupPost);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPost _actualGroupPost = _groupPostBL.PostNewPostToGroup(_expectedGroupPost);

        //Assert
        Assert.Same(_expectedGroupPost, _actualGroupPost);
    }

    [Fact]
    public void Should_Update_Group_Post()
    {
        //Arrange
        GroupPost _expectedGroupPost = new GroupPost()
        {
            GroupPostId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateGroupPost(_expectedGroupPost)).Returns(_expectedGroupPost);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPost _actualGroupPost = _groupPostBL.UpdateGroupPost(_expectedGroupPost);

        //Assert
        Assert.Same(_expectedGroupPost, _actualGroupPost);
    }

    [Fact]
    public void Should_Delete_Group_Post()
    {
        //Arrange
        GroupPost _expectedGroupPost = new GroupPost()
        {
            GroupPostId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Hello World!",
            PostPhoto = "https://previews.123rf.com/images/fordzolo/fordzolo1506/fordzolo150600296/41026708-example-white-stamp-text-on-red-backgroud.jpg",
            IsDeleted = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.DeleteGroupPost(_expectedGroupPost)).Returns(_expectedGroupPost);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPost _actualGroupPost = _groupPostBL.DeleteGroupPost(_expectedGroupPost);

        //Assert
        Assert.Same(_expectedGroupPost, _actualGroupPost);
    }

    [Fact]
    public void Should_Add_New_Group_Post_Comment()
    {
        //Arrange
        GroupPostComment _expectedGroupPostComment = new GroupPostComment()
        {
            GroupPostCommentId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            GroupPostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            GroupPostComment1 = "Hello World!",
            IsDeleted = false,
            Group = new Group(),
            GroupPost = new GroupPost(),
            User = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewGroupPostComment(_expectedGroupPostComment)).Returns(_expectedGroupPostComment);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPostComment _actualGroupPost = _groupPostBL.AddNewGroupPostComment(_expectedGroupPostComment);

        //Assert
        Assert.Same(_expectedGroupPostComment, _actualGroupPost);
    }

    [Fact]
    public void Should_Update_Group_Post_Comment()
    {
        //Arrange
        GroupPostComment _expectedGroupPostComment = new GroupPostComment()
        {
            GroupPostCommentId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            GroupPostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            GroupPostComment1 = "Hello World!",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateGroupPostComment(_expectedGroupPostComment)).Returns(_expectedGroupPostComment);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPostComment _actualGroupPost = _groupPostBL.UpdateGroupPostComment(_expectedGroupPostComment);

        //Assert
        Assert.Same(_expectedGroupPostComment, _actualGroupPost);
    }

    [Fact]
    public void Should_Delete_Group_Post_Comment()
    {
        //Arrange
        GroupPostComment _expectedGroupPostComment = new GroupPostComment()
        {
            GroupPostCommentId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            GroupPostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            GroupPostComment1 = "Hello World!",
            IsDeleted = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.DeleteGroupPostComment(_expectedGroupPostComment)).Returns(_expectedGroupPostComment);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPostComment _actualGroupPost = _groupPostBL.DeleteGroupPostComment(_expectedGroupPostComment);

        //Assert
        Assert.Same(_expectedGroupPostComment, _actualGroupPost);
    }

    [Fact]
    public void Should_React_To_Group_Post()
    {
        //Arrange
        GroupPostReact _expectedGroupPostReact = new GroupPostReact()
        {
            GroupPostReactId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            GroupPostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            ReactId = Guid.NewGuid().ToString(),
            Group = new Group(),
            GroupPost = new GroupPost(),
            React = new Reaction(),
            User = new ApplicationUser()
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.ReactGroupPost(_expectedGroupPostReact)).Returns(_expectedGroupPostReact);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPostReact _actualGroupPost = _groupPostBL.ReactGroupPost(_expectedGroupPostReact);

        //Assert
        Assert.Same(_expectedGroupPostReact, _actualGroupPost);
    }

    [Fact]
    public void Should_React_To_Group_Post_Comment()
    {
        //Arrange
        GroupPostCommentReact _expectedGroupPostCommentReact = new GroupPostCommentReact()
        {
            GroupPostCommentReactId = Guid.NewGuid().ToString(),
            GroupId = Guid.NewGuid().ToString(),
            GroupPostCommentId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            ReactId = Guid.NewGuid().ToString(),
            Group = new Group(),
            GroupPostComment = new GroupPostComment(),
            React = new Reaction(),
            User = new ApplicationUser()
        };

        Mock<IGroupPostManagementDL> _mockRepo = new Mock<IGroupPostManagementDL>();
        _mockRepo.Setup(repo => repo.ReactGroupPostComment(_expectedGroupPostCommentReact)).Returns(_expectedGroupPostCommentReact);
        IGroupPostManagementBL _groupPostBL = new GroupPostManagementBL(_mockRepo.Object);

        //Act
        GroupPostCommentReact _actualGroupPostComment = _groupPostBL.ReactGroupPostComment(_expectedGroupPostCommentReact);

        //Assert
        Assert.Same(_expectedGroupPostCommentReact, _actualGroupPostComment);
    }
}