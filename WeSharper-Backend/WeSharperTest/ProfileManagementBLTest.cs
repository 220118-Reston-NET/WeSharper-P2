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

public class ProfileManagementBLTest
{
    [Fact]
    public void Should_Update_A_Profile(){
        //Arrange
        string profileId = Guid.NewGuid().ToString();
        string userId = Guid.NewGuid().ToString();
        string firstName = "John";
        string lastName = "Smith";
        string profilePictureUrl = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg";
        string bio = "DNE";
        DateTime createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

        Profile p = new Profile(){
            ProfileId = profileId,
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            ProfilePictureUrl = profilePictureUrl,
            Bio = bio,
            CreatedAt = createdAt
        };

        //string profileIdV2 = Guid.NewGuid().ToString();
        //string userIdV2 = Guid.NewGuid().ToString();
        string firstNameV2 = "JohnV2";
        string lastNameV2 = "SmithV2";
        string profilePictureUrlV2 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTD7Q8VGyccQbLH5kWhpABbR9GjC6cK-jO9bg&usqp=CAU";
        string bioV2 = "DNEV2";
        //DateTime createdAtV2 = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

        Profile p2 = new Profile(){
            ProfileId = profileId,
            UserId = userId,
            FirstName = firstNameV2,
            LastName = lastNameV2,
            ProfilePictureUrl = profilePictureUrlV2,
            Bio = bioV2,
            CreatedAt = createdAt
        };



        Mock<IProfileManagementDL> _mockRepo = new Mock<IProfileManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateProfile(p2)).Returns(p2);
        IProfileManagementBL profileBL = new ProfileManagementBL(_mockRepo.Object);

        //act
        Profile actualProfile = profileBL.UpdateProfile(p2);

        //Assert
        Assert.Same(p2, actualProfile);
        // Assert.Equal(profileId, actualProfile.ProfileId);
        // Assert.Equal(userId, actualProfile.UserId);
        // Assert.Equal(firstNameV2, actualProfile.FirstName);
        // Assert.Equal(lastNameV2, actualProfile.LastName);
        // Assert.Equal(profilePictureUrlV2,actualProfile.ProfilePictureUrl);
        // Assert.Equal(bioV2,actualProfile.Bio);
        // Assert.Equal(createdAt,actualProfile.CreatedAt);
    }

    [Fact]
    public void Should_Get_All_Profiles(){
        //Arrange
        string profileId = Guid.NewGuid().ToString();
        string userId = Guid.NewGuid().ToString();
        string firstName = "John";
        string lastName = "Smith";
        string profilePictureUrl = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg";
        string bio = "DNE";
        DateTime createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

        Profile p = new Profile(){
            ProfileId = profileId,
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            ProfilePictureUrl = profilePictureUrl,
            Bio = bio,
            CreatedAt = createdAt
        };

        
        List<Profile> _expectedListOfProfiles = new List<Profile>();
        _expectedListOfProfiles.Add(p);

        Mock<IProfileManagementDL> _mockRepo = new Mock<IProfileManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllProfiles()).Returns(_expectedListOfProfiles);
        IProfileManagementBL profileBL = new ProfileManagementBL(_mockRepo.Object);

        //act
        List<Profile> _actualListOfProfiles = profileBL.GetAllProfiles();

        //Assert
        Assert.Same(_expectedListOfProfiles, _actualListOfProfiles);
        Assert.Equal(profileId, _actualListOfProfiles[0].ProfileId);
        Assert.Equal(userId, _actualListOfProfiles[0].UserId);
        Assert.Equal(firstName, _actualListOfProfiles[0].FirstName);
        Assert.Equal(lastName, _actualListOfProfiles[0].LastName);
        Assert.Equal(profilePictureUrl,_actualListOfProfiles[0].ProfilePictureUrl);
        Assert.Equal(bio,_actualListOfProfiles[0].Bio);
        Assert.Equal(createdAt,_actualListOfProfiles[0].CreatedAt);
    }

    [Fact]
    public void Should_Get_A_Profile(){
        //Arrange
        string profileId = Guid.NewGuid().ToString();
        string userId = Guid.NewGuid().ToString();
        string firstName = "John";
        string lastName = "Smith";
        string profilePictureUrl = "https://i.kym-cdn.com/entries/icons/facebook/000/027/475/Screen_Shot_2018-10-25_at_11.02.15_AM.jpg";
        string bio = "DNE";
        DateTime createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

        Profile p = new Profile(){
            ProfileId = profileId,
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            ProfilePictureUrl = profilePictureUrl,
            Bio = bio,
            CreatedAt = createdAt
        };

        string profileIdV2 = Guid.NewGuid().ToString();
        string userIdV2 = Guid.NewGuid().ToString();
        string firstNameV2 = "JohnV2";
        string lastNameV2 = "SmithV2";
        string profilePictureUrlV2 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTD7Q8VGyccQbLH5kWhpABbR9GjC6cK-jO9bg&usqp=CAU";
        string bioV2 = "DNEV2";
        DateTime createdAtV2 = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

        Profile p2 = new Profile(){
            ProfileId = profileIdV2,
            UserId = userIdV2,
            FirstName = firstNameV2,
            LastName = lastNameV2,
            ProfilePictureUrl = profilePictureUrlV2,
            Bio = bioV2,
            CreatedAt = createdAtV2
        };

        

        List<Profile> _expectedListOfProfiles = new List<Profile>();
        _expectedListOfProfiles.Add(p);
        _expectedListOfProfiles.Add(p2);

        Mock<IProfileManagementDL> _mockRepo = new Mock<IProfileManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllProfiles()).Returns(_expectedListOfProfiles);
        IProfileManagementBL _profileBL = new ProfileManagementBL(_mockRepo.Object);

        //act
        Profile _actualProfile = new Profile();
        _actualProfile = _profileBL.GetAProfile(p.UserId);

        //Assert
        //Assert.Same(_expectedListOfProfiles[0], _actualProfile);
        Assert.Equal(profileId, _actualProfile.ProfileId);
        Assert.Equal(userId, _actualProfile.UserId);
        Assert.Equal(firstName, _actualProfile.FirstName);
        Assert.Equal(lastName, _actualProfile.LastName);
        Assert.Equal(profilePictureUrl, _actualProfile.ProfilePictureUrl);
        Assert.Equal(bio, _actualProfile.Bio);
        Assert.Equal(createdAt, _actualProfile.CreatedAt);
    } 
}