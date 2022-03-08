
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

public class HobbyManagementBLTest
{
    [Fact]
    public void Should_Update_A_Hobby(){
        //Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "John";
        
        Hobby h = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        string hobbyIdV2 = Guid.NewGuid().ToString();
        string hobbyNameV2 = "Joe";
        
        Hobby h2 = new Hobby(){
            HobbyId = hobbyIdV2,
            HobbyName = hobbyNameV2
        };



        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateHobby(h2)).Returns(h2);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //act
        Hobby actualHobby = hobbyBL.UpdateHobby(h2);

        //Assert
        Assert.Same(h2, actualHobby);
    }

    [Fact]
    public void Should_Get_All_Hobbys(){
        //Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "John";
        
        Hobby h = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        
        List<Hobby> _expectedListOfHobbys = new List<Hobby>();
        _expectedListOfHobbys.Add(h);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbys);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //act
        List<Hobby> _actualListOfHobbies = hobbyBL.GetAllHobbies();

        //Assert
        Assert.Same(_expectedListOfHobbys, _actualListOfHobbies);
        Assert.Equal(hobbyId, _actualListOfHobbies[0].HobbyId);
        Assert.Equal(hobbyName, _actualListOfHobbies[0].HobbyName );
    }

}
