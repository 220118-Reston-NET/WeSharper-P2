
// using Moq;
// using System;
// using System.Collections.Generic;
// using Xunit;
// using WeSharper.Models;
// using WeSharper.BusinessesManagement.Interfaces;
// using WeSharper.DatabaseManagement.Interfaces;
// using WeSharper.BusinessesManagement.Implements;
// using WeSharper.DatabaseManagement.Implements;
// using System.Threading.Tasks;

// namespace WeSharper.Test;

// public class HobbyManagementBLTest
// {
//     [Fact]
//     public async Task Should_Update_A_Hobby()
//     {
//         //Arrange
//         string hobbyId = Guid.NewGuid().ToString();
//         string hobbyName = "Coding";

//         Hobby hobby1 = new Hobby()
//         {
//             HobbyId = hobbyId,
//             HobbyName = hobbyName
//         };

//         string hobbyIdV2 = Guid.NewGuid().ToString();
//         string hobbyNameV2 = "Joe";

//         Hobby hobby2 = new Hobby()
//         {
//             HobbyId = hobbyIdV2,
//             HobbyName = hobbyNameV2
//         };



//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.UpdateHobby(hobby2)).ReturnsAsync(hobby2);
//         IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         //act
//         Hobby actualHobby = await hobbyBL.UpdateHobby(hobby2);

//         //Assert
//         Assert.Same(hobby2, actualHobby);
//     }

//     [Fact]
//     public async Task Should_Get_All_Hobbies()
//     {
//         //Arrange
//         string hobbyId = Guid.NewGuid().ToString();
//         string hobbyName = "Coding";

//         Hobby hobby1 = new Hobby()
//         {
//             HobbyId = hobbyId,
//             HobbyName = hobbyName
//         };


//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();
//         _expectedListOfHobbies.Add(hobby1);

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.GetAllHobbies()).ReturnsAsync(_expectedListOfHobbies);
//         IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         //act
//         List<Hobby> _actualListOfHobbies = await hobbyBL.GetAllHobbies();

//         //Assert
//         Assert.Same(_expectedListOfHobbies, _actualListOfHobbies);
//         Assert.Equal(hobbyId, _actualListOfHobbies[0].HobbyId);
//         Assert.Equal(hobbyName, _actualListOfHobbies[0].HobbyName);
//     }

//     [Fact]
//     public async Task Should_Not_Add_A_Hobby_Because_Duplicate_Value()
//     {
//         // Arrange
//         string hobbyId = Guid.NewGuid().ToString();
//         string hobbyName = "Coding";

//         Hobby hobby1 = new Hobby()
//         {
//             HobbyId = hobbyId,
//             HobbyName = hobbyName
//         };

//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();
//         _expectedListOfHobbies.Add(hobby1);

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.AddNewHobby(hobby1)).ReturnsAsync(hobby1);
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         // Act & Assert
//         await Assert.ThrowsAsync<System.Exception>(
//             () => _hobbyBL.AddNewHobby(hobby1)
//         );
//     }

//     [Fact]
//     public async Task Should_Not_Add_A_Hobby_Because_Invalid_Hobby_Name()
//     {
//         // Arrange
//         string hobbyId = Guid.NewGuid().ToString();
//         string hobbyName = "5dfas51651616df";

//         Hobby hobby1 = new Hobby()
//         {
//             HobbyId = hobbyId,
//             HobbyName = hobbyName
//         };

//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.AddNewHobby(hobby1)).ReturnsAsync(hobby1);
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         Hobby _newHobby = new Hobby();

//         // Act and assert
//         await Assert.ThrowsAsync<System.Exception>(
//             () => _hobbyBL.AddNewHobby(hobby1)
//         );
//     }

//     [Fact]
//     public async Task Should_Delete_A_Hobby()
//     {
//         //Arrange
//         string hobbyId = Guid.NewGuid().ToString();
//         string hobbyName = "Coding";

//         Hobby hobby1 = new Hobby()
//         {
//             HobbyId = hobbyId,
//             HobbyName = hobbyName
//         };

//         string hobbyIdV2 = Guid.NewGuid().ToString();
//         string hobbyNameV2 = "Joe";

//         Hobby hobby2 = new Hobby()
//         {
//             HobbyId = hobbyIdV2,
//             HobbyName = hobbyNameV2
//         };

//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();
//         _expectedListOfHobbies.Add(hobby1);
//         _expectedListOfHobbies.Add(hobby2);

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.DeleteHobby(hobby2)).ReturnsAsync(hobby2);
//         _mockRepo.Setup(repo => repo.GetAllHobbies()).ReturnsAsync(_expectedListOfHobbies);
//         IHobbyManagementBL hobbyBL = new HobbyManagementBL(_mockRepo.Object);


//         //act
//         Hobby deletedHobby = await hobbyBL.DeleteHobby(hobby2);

//         // //Assert
//         Assert.Equal(hobby2, deletedHobby);
//     }



//     [Fact]
//     public async Task Should_Add_A_New_Hobby()
//     {
//         // Arrange
//         Hobby _expectedHobby = new Hobby()
//         {
//             HobbyId = Guid.NewGuid().ToString(),
//             HobbyName = "Coding"
//         };

//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();
//         _expectedListOfHobbies.Add(_expectedHobby);

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.GetAllHobbies()).ReturnsAsync(new List<Hobby> { });
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         //Act
//         Hobby _actualHobby = await _hobbyBL.AddNewHobby(_expectedHobby);

//         // Assert
//         Assert.Same(_expectedHobby, _actualHobby);
//     }

//     [Fact]
//     public async Task Should_Validate_Hobby_Name()
//     {
//         string _hobbyName = "Running";

//         Hobby _expectedHobby = new Hobby()
//         {
//             HobbyId = Guid.NewGuid().ToString(),
//             HobbyName = "Coding"
//         };
//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.AddNewHobby(_expectedHobby)).ReturnsAsync(_expectedHobby);
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         //Act
//         bool nameValid = await _hobbyBL.ValidHobbyName(_hobbyName);

//         // Assert
//         Assert.True(nameValid);
//     }

//     [Fact]
//     public async Task Should_Not_Validate_Hobby_Name()
//     {
//         string _invalidHobbyName = "C0ding";

//         Hobby _expectedHobby = new Hobby()
//         {
//             HobbyId = Guid.NewGuid().ToString(),
//             HobbyName = "Coding"
//         };
//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.AddNewHobby(_expectedHobby)).ReturnsAsync(_expectedHobby);
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         // Act & Assert
//         await Assert.ThrowsAsync<System.Exception>(
//             () => _hobbyBL.ValidHobbyName(_invalidHobbyName)
//         );
//     }

//     [Fact]
//     public async Task Fails_Checks_for_Dupilcate_Value()
//     {

//         Hobby _expectedHobby = new Hobby()
//         {
//             HobbyId = Guid.NewGuid().ToString(),
//             HobbyName = "Coding"
//         };

//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();
//         _expectedListOfHobbies.Add(_expectedHobby);

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.GetAllHobbies()).ReturnsAsync(_expectedListOfHobbies);
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         // Act & Assert
//         await Assert.ThrowsAsync<System.Exception>(
//             () => _hobbyBL.CheckDuplicateHobby("Coding")
//         );
//     }

//     [Fact]
//     public async Task Checks_for_Dupilcate_Value()
//     {
//         Hobby _expectedHobby = new Hobby()
//         {
//             HobbyId = Guid.NewGuid().ToString(),
//             HobbyName = "Coding"
//         };

//         List<Hobby> _expectedListOfHobbies = new List<Hobby>();
//         _expectedListOfHobbies.Add(_expectedHobby);

//         Mock<IHobbyManagementDL> _mockRepo = new Mock<IHobbyManagementDL>();
//         _mockRepo.Setup(repo => repo.GetAllHobbies()).ReturnsAsync(_expectedListOfHobbies);
//         IHobbyManagementBL _hobbyBL = new HobbyManagementBL(_mockRepo.Object);

//         // Act & Assert
//         bool validHobby = await _hobbyBL.CheckDuplicateHobby("Running");
//         // Assert
//         Assert.True(validHobby);
//     }
// }
