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

public class ProfileManagementBLTest
{
    [Fact]
    public async Task Should_Update_A_Profile()
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
        _mockRepo.Setup(repo => repo.UpdateProfile(profile1)).ReturnsAsync(profile1);
        IProfileManagementBL profileBL = new ProfileManagementBL(_mockRepo.Object);

        //Act
        Profile actualProfile = await profileBL.UpdateProfile(profile1);

        //Assert
        Assert.Same(profile1, actualProfile);
        Assert.Equal(profile1.FirstName, actualProfile.FirstName);
        Assert.Equal(profile1.LastName, actualProfile.LastName);
    }

    [Fact]
    public async Task Should_Get_All_Profiles()
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
        _mockRepo.Setup(repo => repo.GetAllProfiles()).ReturnsAsync(_expectedListOfProfiles);
        IProfileManagementBL profileBL = new ProfileManagementBL(_mockRepo.Object);

        //act
        List<Profile> _actualListOfProfiles = await profileBL.GetAllProfiles();

        //Assert
        Assert.Same(_expectedListOfProfiles, _actualListOfProfiles);
    }

    [Fact]
    public async Task Should_Get_A_New_Profile()
    {
        //Arrange
        Profile _expectedProfile = new Profile()
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
        _mockRepo.Setup(repo => repo.AddNewProfile(_expectedProfile)).ReturnsAsync(_expectedProfile);
        IProfileManagementBL _profileBL = new ProfileManagementBL(_mockRepo.Object);

        //Act
        Profile _actualProfile = new Profile();
        _actualProfile = await _profileBL.AddNewProfile(_expectedProfile);

        //Assert
        Assert.Same(_expectedProfile, _actualProfile);

    }
}
