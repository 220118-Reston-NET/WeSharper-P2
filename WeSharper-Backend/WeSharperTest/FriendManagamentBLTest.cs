
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;
using WeSharper.DatabaseManagement.Implements;

namespace WeSharper.Test;

public class FriendManagementBLTest
{
    [Fact]
    public void Should_Add_A_New_Friend()
    {
        // Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Strangers",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).Returns( new List<Friend>{});
        _mockRepo.Setup(repo => repo.AddFriend(_expectedFriend)).Returns(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        Friend _actualFriend = _friendBL.SendFriendRequest(_expectedFriend);
        
        // Assert
        Assert.Same(_expectedFriend,_actualFriend);
    }

    [Fact]
    public void Should_Not_Add_A_New_Friend_Because_Duplicate()
    {
        // Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Strangers",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Friend> _expectedListOfFriends = new List<Friend>();
        _expectedListOfFriends.Add(_expectedFriend);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).Returns( _expectedListOfFriends );
        _mockRepo.Setup(repo => repo.AddFriend(_expectedFriend)).Returns(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        // Act & Assert
        Assert.Throws<System.Exception>(
            () => _friendBL.SendFriendRequest(_expectedFriend)
        );
    }

    [Fact]
    public void Should_Get_All_Friends()
    {
        // Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Strangers",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Friend> _expectedListOfFriends = new List<Friend>();
        _expectedListOfFriends.Add(_expectedFriend);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).Returns( _expectedListOfFriends );
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Friend> _actualListOfFriends = _friendBL.GetAllFriends();
        
        // Assert
        Assert.Same(_expectedListOfFriends,_actualListOfFriends);
    }

    [Fact]
    public void Fail_Get_All_Friends_Because_No_Friends()
    {
        // Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Strangers",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Friend> _expectedListOfFriends = new List<Friend>();
        _expectedListOfFriends.Add(_expectedFriend);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).Returns( new List<Friend>{} );
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act & Assert
        Assert.Throws<Exception>(() => _friendBL.GetAllFriends() );
    }

    [Fact]
    public void Should_Get_User_Friends()
    {
        // Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Strangers",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Friend> _expectedListOfFriends = new List<Friend>();
        _expectedListOfFriends.Add(_expectedFriend);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).Returns( _expectedListOfFriends );
        //_mockRepo.Setup(repo => repo.AddFriend(_expectedFriend)).Returns(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        //Act
        List<Friend> _actualListOfFriends = _friendBL.GetAllFriends();
        
        // Assert
        Assert.Same(_expectedListOfFriends,_actualListOfFriends);
    }

    [Fact]
    public void Should_Not_Get_User_Friends_Because_DNE()
    {
        // Arrange
        Friend _expectedFriend = new Friend()
        {
            RelationshipId = Guid.NewGuid().ToString(),
            RequestedUserId = Guid.NewGuid().ToString(),
            AcceptedUserId = Guid.NewGuid().ToString(),
            IsAccepted = false,
            Relationship = "Strangers",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Friend> _expectedListOfFriends = new List<Friend>();
        _expectedListOfFriends.Add(_expectedFriend);

        Mock<IFriendManagementDL> _mockRepo = new Mock<IFriendManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllFriends()).Returns( _expectedListOfFriends );
        //_mockRepo.Setup(repo => repo.AddFriend(_expectedFriend)).Returns(_expectedFriend);
        IFriendManagementBL _friendBL = new FriendManagementBL(_mockRepo.Object);

        Assert.Throws<Exception>(() => _friendBL.GetUserFriends("asdf") );
    }
    
      
} 
