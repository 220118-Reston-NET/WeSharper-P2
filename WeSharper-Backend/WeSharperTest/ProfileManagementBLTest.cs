using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;

namespace WeSharper.Test;

public class ProfileManagementBLTest
{
    [Fact]
    public void Should_Update_A_Profile()
    {
        //Arrange
        Profile profile1 = new Profile()
        {
            ProfileId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Smith",
            ProfilePictureUrl = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Bio = "DNE",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Mock<IProfileManagementDL> _mockRepo = new Mock<IProfileManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateProfile(profile1)).Returns(profile1);
        IProfileManagementBL profileBL = new ProfileManagementBL(_mockRepo.Object);

        //Act
        Profile actualProfile = profileBL.UpdateProfile(profile1);

        //Assert
        Assert.Same(profile1, actualProfile);
        Assert.Equal(profile1.FirstName, actualProfile.FirstName);
        Assert.Equal(profile1.LastName, actualProfile.LastName);
    }

    [Fact]
    public void Should_Get_All_Profiles()
    {
        //Arrange
        Profile profile1 = new Profile()
        {
            ProfileId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Smith",
            ProfilePictureUrl = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Bio = "DNE",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Profile> _expectedListOfProfiles = new List<Profile>();
        _expectedListOfProfiles.Add(profile1);

        Mock<IProfileManagementDL> _mockRepo = new Mock<IProfileManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllProfiles()).Returns(_expectedListOfProfiles);
        IProfileManagementBL profileBL = new ProfileManagementBL(_mockRepo.Object);

        //act
        List<Profile> _actualListOfProfiles = profileBL.GetAllProfiles();

        //Assert
        Assert.Same(_expectedListOfProfiles, _actualListOfProfiles);
    }

    [Fact]
    public void Should_Get_A_Profile()
    {
        //Arrange
        Profile profile1 = new Profile()
        {
            ProfileId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Smith",
            ProfilePictureUrl = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg",
            Bio = "DNE",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        Profile profile2 = new Profile()
        {
            ProfileId = Guid.NewGuid().ToString(),
            UserId = Guid.NewGuid().ToString(),
            FirstName = "JohnV2",
            LastName = "SmithV2",
            ProfilePictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTD7Q8VGyccQbLH5kWhpABbR9GjC6cK-jO9bg&usqp=CAU",
            Bio = "DNEV2",
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))
        };

        List<Profile> _expectedListOfProfiles = new List<Profile>();
        _expectedListOfProfiles.Add(profile1);
        _expectedListOfProfiles.Add(profile2);

        Mock<IProfileManagementDL> _mockRepo = new Mock<IProfileManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllProfiles()).Returns(_expectedListOfProfiles);
        IProfileManagementBL _profileBL = new ProfileManagementBL(_mockRepo.Object);

        //Act
        Profile _actualProfile = new Profile();
        _actualProfile = _profileBL.GetAProfile(profile2.UserId);

        //Assert
        Assert.Same(profile2, _actualProfile);
        Assert.Equal(profile2.ProfileId, _actualProfile.ProfileId);
        Assert.Equal(profile2.FirstName, _actualProfile.FirstName);
    }
}