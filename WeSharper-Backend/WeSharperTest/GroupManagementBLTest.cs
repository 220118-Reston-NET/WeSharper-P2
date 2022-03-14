
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;
using WeSharper.DatabaseManagement.Implements;
using System.Threading.Tasks;

namespace WeSharper.Test;

public class GroupManagementBLTest
{
    [Fact]
    public async Task Should_Add_A_Group()
    {
        //Arrange
        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            GroupManager = new ApplicationUser(),
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.AddGroup(_expectedGroup)).ReturnsAsync(_expectedGroup);
        IGroupManagementBL groupBL = new GroupManagementBL(_mockRepo.Object);

        //act
        Group actualGroup = await groupBL.AddNewGroup(_expectedGroup);

        //Assert
        Assert.Same(_expectedGroup, actualGroup);
    }

    [Fact]
    public async Task Should_Not_Get_All_Groups_Because_No_Groups()
    {
        // Arrange
        List<Group> _expectedListOfGroups = new List<Group>();

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        await Assert.ThrowsAsync<Exception>(() => _groupBL.GetAllGroups());
    }

    [Fact]
    public async Task Should_Get_All_Groups()
    {
        // Arrange
        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Group> _expectedListOfGroups = new List<Group>();
        _expectedListOfGroups.Add(_expectedGroup);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        //_mockRepo.Setup(repo => repo.AddGroup(_expectedGroup)).ReturnsAsync(_expectedGroup);
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        //Act
        List<Group> _actualListOfGroups = await _groupBL.GetAllGroups();

        // Assert
        Assert.Same(_expectedListOfGroups, _actualListOfGroups);
    }

    [Fact]
    public async Task Fail_GroupId_Check()
    {
        // Arrange
        List<Group> _expectedListOfGroups = new List<Group>();

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        await Assert.ThrowsAsync<Exception>(() => _groupBL.CheckGroupId("45165"));
    }

    [Fact]
    public async Task Pass_GroupId_Check()
    {
        // Arrange
        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Group> _expectedListOfGroups = new List<Group>();
        _expectedListOfGroups.Add(_expectedGroup);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        //_mockRepo.Setup(repo => repo.AddGroup(_expectedGroup)).ReturnsAsync(_expectedGroup);
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        //Act
        Group _actualGroups = await _groupBL.CheckGroupId(_expectedGroup.GroupId);

        // Assert
        Assert.Same(_expectedGroup, _actualGroups);
    }

    [Fact]
    public async Task Fail_Group_Manager_Check()
    {
        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Group> _expectedListOfGroups = new List<Group>();
        _expectedListOfGroups.Add(_expectedGroup);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        await Assert.ThrowsAsync<Exception>(() => _groupBL.CheckGroupManager(_expectedGroup.GroupId, Guid.NewGuid().ToString()));
    }

    [Fact]
    public async Task Pass_Group_Manager_Check()
    {
        // Arrange
        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Group> _expectedListOfGroups = new List<Group>();
        _expectedListOfGroups.Add(_expectedGroup);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        //_mockRepo.Setup(repo => repo.AddGroup(_expectedGroup)).ReturnsAsync(_expectedGroup);
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        //Act
        Group _actualGroups = await _groupBL.CheckGroupManager(_expectedGroup.GroupId, _expectedGroup.GroupManagerId);

        // Assert
        Assert.Same(_expectedGroup, _actualGroups);
    }

    [Fact]
    public async Task Should_Update_A_Group()
    {

        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Group> _expectedListOfGroups = new List<Group>();
        _expectedListOfGroups.Add(_expectedGroup);


        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        _mockRepo.Setup(repo => repo.UpdateGroup(_expectedGroup, _expectedGroup.GroupManagerId)).ReturnsAsync(_expectedGroup);
        IGroupManagementBL groupBL = new GroupManagementBL(_mockRepo.Object);

        //act
        Group actualGroup = await groupBL.UpdateGroupInformation(_expectedGroup, _expectedGroup.GroupManagerId);

        //Assert
        Assert.Same(_expectedGroup, actualGroup);
    }

    // [Fact]
    // public async Task Should_Not_Update_A_Group()
    // {

    //     Group _expectedGroup = new Group()
    //     {
    //         GroupId = Guid.NewGuid().ToString(),
    //         GroupManagerId = Guid.NewGuid().ToString(),
    //         GroupName = "TestGroup",
    //         GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
    //         Description = "TestDescription",
    //         IsActivated = true,
    //         CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
    //     };

    //     List<Group> _expectedListOfGroups = new List<Group>();
    //     _expectedListOfGroups.Add(_expectedGroup);


    //     Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
    //     _mockRepo.Setup(repo => repo.UpdateGroup(_expectedGroup, _expectedGroup.GroupManagerId)).ReturnsAsync(_expectedGroup);
    //     IGroupManagementBL groupBL = new GroupManagementBL(_mockRepo.Object);


    //     Assert.ThrowsAsync<Exception>(
    //         () => await groupBL.UpdateGroupInformation(_expectedGroup, "1561"));
    // }

    [Fact]
    public async Task Should_Delete_Group()
    {
        Group _expectedGroup = new Group()
        {
            GroupId = Guid.NewGuid().ToString(),
            GroupManagerId = Guid.NewGuid().ToString(),
            GroupName = "TestGroup",
            GroupPicture = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Description = "TestDescription",
            IsActivated = true,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Group> _expectedListOfGroups = new List<Group>();
        _expectedListOfGroups.Add(_expectedGroup);


        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroups()).ReturnsAsync(_expectedListOfGroups);
        _mockRepo.Setup(repo => repo.UpdateGroup(_expectedGroup, _expectedGroup.GroupManagerId)).ReturnsAsync(_expectedGroup);
        _mockRepo.Setup(repo => repo.DeleteGroup(_expectedGroup.GroupId, _expectedGroup.GroupManagerId)).ReturnsAsync(_expectedGroup);
        IGroupManagementBL groupBL = new GroupManagementBL(_mockRepo.Object);

        //act
        Group actualGroup = await groupBL.DeleteGroup(_expectedGroup.GroupId, _expectedGroup.GroupManagerId);

        //Assert
        Assert.Same(_expectedGroup, actualGroup);
    }



    /*
    // GroupUsers
    [Fact]
    public async Task Should_Add_A_Group_User_Request()
    {
        //Arrange
        GroupUser _expectedGroupUser = new GroupUser(){
                    GroupId = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    IsBanned = false,
                    IsApproved = true,
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.SendNewGroupUserRequest(_expectedGroupUser)).ReturnsAsync(_expectedGroupUser);
        IGroupManagementBL groupBL = new GroupManagementBL(_mockRepo.Object);

        //act
        GroupUser actualGroup = groupBL.SendNewGroupUserRequest(_expectedGroupUser);

        //Assert
        Assert.Same(_expectedGroupUser, actualGroup);
    }
    
    [Fact]
    public async Task Should_Not_Get_All_GroupUsers_Because_No_GroupUsers()
    {
        // Arrange
        List<GroupUser> _expectedListOfGroupUsers = new List<GroupUser>();

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroupUsers()).ReturnsAsync( _expectedListOfGroupUsers );
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        Assert.Throws<Exception>(() => _groupBL.GetAllGroupUsers() );
    }

    [Fact]
    public async Task Should_Get_All_GroupUsers()
    {
        //Arrange
        GroupUser _expectedGroupUser = new GroupUser(){
                    GroupId = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    IsBanned = false,
                    IsApproved = true,
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };

        List<GroupUser> _expectedListOfGroupUsers = new List<GroupUser>();
        _expectedListOfGroupUsers.Add(_expectedGroupUser);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroupUsers()).ReturnsAsync( _expectedListOfGroupUsers );
        IGroupManagementBL _groupBL = new GroupManagementBL(_mockRepo.Object);

        //Act
        List<Group> _actualListOfGroupUsers = _groupBL.GetAllGroups();
        
        // Assert
        Assert.Same(_expectedListOfGroupUsers,_actualListOfGroupUsers);
    }

    [Fact]
    public async Task Fail_Get_An_Unapproved_Join_Request_Because_DNE()
    {
        //Arrange
        GroupUser _expectedGroupUser = new GroupUser(){
                    GroupId = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    IsBanned = false,
                    IsApproved = true,
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };

        List<GroupUser> _expectedListOfGroupUsers = new List<GroupUser>();
        _expectedListOfGroupUsers.Add(_expectedGroupUser);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroupUsers()).ReturnsAsync(_expectedListOfGroupUsers);
        IGroupManagementBL _profileBL = new GroupManagementBL(_mockRepo.Object);

        Assert.Throws<Exception>(() => _profileBL.GetGroupUnapprovedJoinRequests("asdf") );
        
    } 

    [Fact]
    public async Task Fail_Get_A_GroupUser_Because_No_GroupUsers()
    {
        //Arrange
        GroupUser _expectedGroupUser = new GroupUser(){
                    GroupId = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    IsBanned = false,
                    IsApproved = true,
                    CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
                };

        List<GroupUser> _expectedListOfGroupUsers = new List<GroupUser>();
        _expectedListOfGroupUsers.Add(_expectedGroupUser);

        Mock<IGroupManagementDL> _mockRepo = new Mock<IGroupManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllGroupUsers()).ReturnsAsync(_expectedListOfGroupUsers);
        IGroupManagementBL _profileBL = new GroupManagementBL(_mockRepo.Object);

        Assert.Throws<Exception>(() => _profileBL.GetGroupUnapprovedJoinRequests("asdf") );
        
    } 

    */



}
