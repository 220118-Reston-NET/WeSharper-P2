
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
        string hobbyName = "Coding";
        
        Hobby hobby1 = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        string hobbyIdV2 = Guid.NewGuid().ToString();
        string hobbyNameV2 = "Joe";
        
        Hobby hobby2 = new Hobby(){
            HobbyId = hobbyIdV2,
            HobbyName = hobbyNameV2
        };



        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.UpdateHobby(hobby2)).Returns(hobby2);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //act
        Hobby actualHobby = hobbyBL.UpdateHobby(hobby2);

        //Assert
        Assert.Same(hobby2, actualHobby);
    }

    [Fact]
    public void Should_Get_All_Hobbies(){
        //Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "Coding";
        
        Hobby hobby1 = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        
        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(hobby1);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbies);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //act
        List<Hobby> _actualListOfHobbies = hobbyBL.GetAllHobbies();

        //Assert
        Assert.Same(_expectedListOfHobbies, _actualListOfHobbies);
        Assert.Equal(hobbyId, _actualListOfHobbies[0].HobbyId);
        Assert.Equal(hobbyName, _actualListOfHobbies[0].HobbyName );
    }

    [Fact]
    public void Should_Not_Add_A_Hobby_Because_Duplicate_Value()
    {
        // Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "Coding";
        
        Hobby hobby1 = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(hobby1);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewHobby(hobby1)).Returns(hobby1);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        Hobby _newHobby = new Hobby();
        // Act & Assert
        Assert.Throws<System.Exception>(
            () => _newHobby = _hobbyBL.AddNewHobby(hobby1)
        );
    }

    [Fact]
    public void Should_Not_Add_A_Hobby_Because_Invalid_Hobby_Name()
    {
        // Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "5dfas51651616df";
        
        Hobby hobby1 = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewHobby(hobby1)).Returns(hobby1);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        Hobby _newHobby = new Hobby();

        // Act and assert
        Assert.Throws<System.Exception>(
            () => _newHobby = _hobbyBL.AddNewHobby(hobby1)
        );
    }
    
    [Fact]
    public void Should_Delete_A_Hobby(){
        //Arrange
        string hobbyId = Guid.NewGuid().ToString();
        string hobbyName = "Coding";
        
        Hobby hobby1 = new Hobby(){
            HobbyId = hobbyId,
            HobbyName = hobbyName
        };

        string hobbyIdV2 = Guid.NewGuid().ToString();
        string hobbyNameV2 = "Joe";
        
        Hobby hobby2 = new Hobby(){
            HobbyId = hobbyIdV2,
            HobbyName = hobbyNameV2
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(hobby1);
        _expectedListOfHobbies.Add(hobby2);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.DeleteHobby(hobby2)).Returns(hobby2);
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbies);
        IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        
        //act
        Hobby deletedHobby = hobbyBL.DeleteHobby(hobby2);

        // //Assert
        Assert.Equal(hobby2,deletedHobby);
    } 

    

    [Fact]
    public void Should_Add_A_New_Hobby()
    {
        // Arrange
        Hobby _expectedHobby = new Hobby(){
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "Coding"
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(_expectedHobby);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns( new List<Hobby>{});
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //Act
        Hobby _actualHobby = _hobbyBL.AddNewHobby(_expectedHobby);
        
        // Assert
        Assert.Same(_expectedHobby,_actualHobby);
    }

    [Fact]
    public void Should_Validate_Hobby_Name()
    {
        string _hobbyName = "Running";

        Hobby _expectedHobby = new Hobby(){
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "Coding"
        };
        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewHobby(_expectedHobby)).Returns(_expectedHobby);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

        //Act
        bool nameValid = _hobbyBL.ValidHobbyName(_hobbyName) ;
        
        // Assert
        Assert.True(nameValid);
    }

    [Fact]
    public void Should_Not_Validate_Hobby_Name()
    {
        string _invalidHobbyName = "C0ding";

        Hobby _expectedHobby = new Hobby(){
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "Coding"
        };
        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.AddNewHobby(_expectedHobby)).Returns(_expectedHobby);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);
        
        // Act & Assert
        Assert.Throws<System.Exception>(
            () => _hobbyBL.ValidHobbyName(_invalidHobbyName)
        );
    }

    [Fact]
    public void Fails_Checks_for_Dupilcate_Value(){

        Hobby _expectedHobby = new Hobby(){
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "Coding"
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(_expectedHobby);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbies);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);
        
        // Act & Assert
        Assert.Throws<System.Exception>(
            () => _hobbyBL.CheckDuplicateHobby("Coding")
        );
    }
    
    [Fact]
    public void Checks_for_Dupilcate_Value(){
        Hobby _expectedHobby = new Hobby(){
            HobbyId = Guid.NewGuid().ToString(),
            HobbyName = "Coding"
        };

        List<Hobby> _expectedListOfHobbies = new List<Hobby>();
        _expectedListOfHobbies.Add(_expectedHobby);

        Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
        _mockRepo.Setup(repo => repo.GetAllHobbies()).Returns(_expectedListOfHobbies);
        IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);
        
        // Act & Assert
        bool validHobby = _hobbyBL.CheckDuplicateHobby("Running");
        // Assert
        Assert.True(validHobby); 
    }
} 
