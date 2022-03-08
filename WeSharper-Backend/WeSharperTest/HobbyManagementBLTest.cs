
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
    public void Should_Get_All_Hobbies(){
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

    [Fact]
    public void Should_Not_Add_A_Hobby_Because_Duplicate_Value()
    {
        // Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "John";
        
        Hobby h = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(h);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewHobby(h)).Returns(h);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        Hobby _newHobby = new Hobby();
        // Act & Assert
        // Shouldn't add new customer due to the name is existing in the database
        Assert.Throws<System.Exception>(
            () => _newHobby = _hobbyBL.AddNewHobby(h)
        );
    }

    [Fact]
    public void Should_Not_Add_A_Hobby_Because_Invalid_Hobby_Name()
    {
        // Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "5dfas51651616df";
        
        Hobby h = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewHobby(h)).Returns(h);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        Hobby _newHobby = new Hobby();
        // Act & Assert
        // Shouldn't add new customer due to the name is existing in the database
        Assert.Throws<System.Exception>(
            () => _newHobby = _hobbyBL.AddNewHobby(h)
        );
    }
    
    [Fact]
    public void Should_Delete_A_Hobby(){
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

        List<Hobby> _expectedListOfHobbys = new List<Hobby>();
        _expectedListOfHobbys.Add(h);
        _expectedListOfHobbys.Add(h2);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.DeleteHobby(h2)).Returns(h2);
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbys);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        
        //act
        Hobby deletedHobby = hobbyBL.DeleteHobby(h2);

        // //Assert
        Assert.Equal(h2,deletedHobby);
    } 

}
