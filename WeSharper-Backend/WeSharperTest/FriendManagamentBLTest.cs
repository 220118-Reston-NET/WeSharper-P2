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

public class FriendManagementBLTest
{
    [Fact]
    public async Task Should_Get_All_Friends()
    {
        //Arrange
        Friend _friend1 = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = true,
            Relationship = "Friend",
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Friend> _expectedListFriends = new List<Friend>();
        _expectedListFriends.Add(_friend1);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).ReturnsAsync(_expectedListFriends);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Friend> _actualListFriends = await _friendBL.GetAllFriends();

        //Assert
        Assert.Same(_expectedListFriends, _actualListFriends);
    }

    [Fact]
    public async Task Should_Get_All_Friends_By_UserID()
    {
        //Arrange
        Friend _friend1 = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = true,
            Relationship = "Friend",
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Friend> _expectedListFriends = new List<Friend>();
        _expectedListFriends.Add(_friend1);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).ReturnsAsync(_expectedListFriends);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Friend> _actualListFriends = await _friendBL.GetAllFriendByUserID(_friend1.AcceptedUserId);

        //Assert
        Assert.Equal(_expectedListFriends[0].AcceptedUserId, _actualListFriends[0].AcceptedUserId);
    }

    [Fact]
    public async Task Should_Get_All_Incoming_Friends_By_UserID()
    {
        //Arrange
        Friend _friend1 = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Friend",
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Friend> _expectedListFriends = new List<Friend>();
        _expectedListFriends.Add(_friend1);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).ReturnsAsync(_expectedListFriends);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Friend> _actualListFriends = await _friendBL.GetAllIncomingFriendByUserID(_friend1.AcceptedUserId);

        //Assert
        Assert.Equal(_expectedListFriends[0].AcceptedUserId, _actualListFriends[0].AcceptedUserId);
    }

    [Fact]
    public async Task Should_Get_All_Outcoming_Friends_By_UserID()
    {
        //Arrange
        Friend _friend1 = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Friend",
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Friend> _expectedListFriends = new List<Friend>();
        _expectedListFriends.Add(_friend1);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).ReturnsAsync(_expectedListFriends);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Friend> _actualListFriends = await _friendBL.GetAllOutcomingFriendByUserID(_friend1.RequestedUserId);

        //Assert
        Assert.Equal(_expectedListFriends[0].AcceptedUserId, _actualListFriends[0].AcceptedUserId);
    }

    [Fact]
    public async Task Should_Get_All_Recommended_Friends_By_UserID()
    {
        //Arrange
        Profile _exptedfriendProfile = new Profile()
        {
            ProfileId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = "FirstNameTest",
            LastName = "LastNameTest",
            ProfilePictureUrl = "imgURL",
            Bio = "Hello World!",
            User = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Profile> _expectedListFriends = new List<Profile>();
        _expectedListFriends.Add(_exptedfriendProfile);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllRecommenedFriendByUserID(_exptedfriendProfile.UserId)).ReturnsAsync(_expectedListFriends);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Profile> _actualListFriends = await _friendBL.GetAllRecommenedFriendByUserID(_exptedfriendProfile.UserId);

        //Assert
        Assert.Same(_actualListFriends, _actualListFriends);
    }

    [Fact]
    public async Task Should_Get_Friend_Profile_By_FriendID()
    {
        //Arrange
        Profile _exptedfriendProfile = new Profile()
        {
            ProfileId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = "FirstNameTest",
            LastName = "LastNameTest",
            ProfilePictureUrl = "imgURL",
            Bio = "Hello World!",
            User = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetFriendProfileByFriendID(_exptedfriendProfile.UserId)).ReturnsAsync(_exptedfriendProfile);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        Profile _actualFriendProfile = await _friendBL.GetFriendProfileByFriendID(_exptedfriendProfile.UserId);

        //Assert
        Assert.Same(_exptedfriendProfile, _actualFriendProfile);
    }

    [Fact]
    public async Task Should_Get_Friend_Posts_By_FriendID()
    {
        //Arrange
        Post _post = new Post()
        {
            PostId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            PostContent = "Content",
            PostPhoto = "PhotoURL",
            IsDeleted = false,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };
        List<Post> _expectedFriendPosts = new List<Post>();
        _expectedFriendPosts.Add(_post);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetFriendPostsByFriendID(_post.UserId)).ReturnsAsync(_expectedFriendPosts);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Post> _actualFriendPosts = await _friendBL.GetFriendPostsByFriendID(_post.UserId);

        //Assert
        Assert.Same(_expectedFriendPosts, _actualFriendPosts);
    }

    [Fact]
    public async Task Should_Add_Friend()
    {
        //Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Friend",
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.AddFriend(_expectedFriend.RequestedUserId, _expectedFriend.AcceptedUserId)).ReturnsAsync(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        Friend _actualFriend = await _friendBL.AddFriend(_expectedFriend.RequestedUserId, _expectedFriend.AcceptedUserId);

        //Assert
        Assert.Same(_expectedFriend, _actualFriend);
    }

    [Fact]
    public async Task Should_Remove_Friend()
    {
        //Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = null,
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.RemoveFriend(_expectedFriend.RequestedUserId, _expectedFriend.AcceptedUserId)).ReturnsAsync(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        Friend _actualFriend = await _friendBL.RemoveFriend(_expectedFriend.RequestedUserId, _expectedFriend.AcceptedUserId);

        //Assert
        Assert.Same(_expectedFriend, _actualFriend);
    }

    [Fact]
    public async Task Should_Accept_Friend()
    {
        //Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = true,
            Relationship = "Friend",
            AcceptedUser = new ApplicationUser(),
            RequestedUser = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.AcceptFriend(_expectedFriend.RequestedUserId, _expectedFriend.AcceptedUserId)).ReturnsAsync(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        Friend _actualFriend = await _friendBL.AcceptFriend(_expectedFriend.RequestedUserId, _expectedFriend.AcceptedUserId);

        //Assert
        Assert.Same(_expectedFriend, _actualFriend);
    }
}
