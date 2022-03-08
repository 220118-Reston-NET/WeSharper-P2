using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using WeSharper.Models;
using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.BusinessesManagement.Implements;

namespace WeSharper.Test;

public class HobbyManagementBLTest
{
    [Fact]
    public void Should_Update_A_Hobby()
    {
        //Arrange
        Hobby hobby1 = new Hobby()
        {
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "John"
        };

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateHobby(hobby1)).Returns(hobby1);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //Act
        Hobby actualHobby = hobbyBL.UpdateHobby(hobby1);

        //Assert
        Assert.Same(hobby1, actualHobby);
    }

    [Fact]
    public void Should_Get_All_Hobbies()
    {
        //Arrange
        Hobby hobby1 = new Hobby()
        {
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "John"
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(hobby1);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbies);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //Act
        List<Hobby> _actualListOfHobbies = hobbyBL.GetAllHobbies();

        //Assert
        Assert.Same(_actualListOfHobbies, _actualListOfHobbies);
    }

}
