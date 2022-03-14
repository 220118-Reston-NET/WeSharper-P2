// using WeSharper.BusinessesManagement.Interfaces;
// using WeSharper.DatabaseManagement.Interfaces;
// using WeSharper.Models;
// using System.Text.RegularExpressions;


// namespace WeSharper.BusinessesManagement.Implements
// {
//     public class HobbyManagementBL : IHobbyManagementBL
//     {
//         private readonly IHobbyManagementDL _repo;

//         public HobbyManagementBL(IHobbyManagementDL repo)
//         {
//             _repo = repo;
//         }

//         public async Task<Hobby> AddNewHobby(Hobby h_hobby)
//         {
//             List<Hobby> _result = await _repo.GetAllHobbies();
//             try
//             {
//                 CheckDuplicateHobby(h_hobby.HobbyName);
//                 _repo.AddNewHobby(h_hobby);
//                 return h_hobby;
//             }
//             catch (System.Exception exe)
//             {
//                 throw new Exception(exe.Message);
//             }
//         }

//         public async Task<bool> ValidHobbyName(string hobbyName)
//         {
//             List<Hobby> _result = await _repo.GetAllHobbies();
//             if (Regex.IsMatch(hobbyName, @"^[a-zA-Z]*$"))
//             {
//                 return true;
//             }
//             throw new Exception("Hobby name is not valid");
//         }
//         public async Task<List<Hobby>> GetAllHobbies()
//         {
//             return await _repo.GetAllHobbies();
//         }

//         public async Task<Hobby> UpdateHobby(Hobby h_hobby)
//         {
//             return await _repo.UpdateHobby(h_hobby);
//         }
//         public async Task<Hobby> DeleteHobby(Hobby h_hobby)
//         {
//             return await _repo.DeleteHobby(h_hobby);
//         }
//         public async Task<bool> CheckDuplicateHobby(string hobbyName)
//         {
//             List<Hobby> _result = await _repo.GetAllHobbies();
//             if (_result.FirstOrDefault(h => h.HobbyName.ToLower() == hobbyName.ToLower()) == null)
//             {
//                 return true;
//             }
//             else
//             {
//                 // Console.WriteLine("Failed");
//                 // Console.WriteLine(_repo.GetAllHobbies().FirstOrDefault(h => h.HobbyName == hobbyName));
//                 throw new Exception(hobbyName + "Duplicate Hobby");
//             }
//         }
//     }
// }
