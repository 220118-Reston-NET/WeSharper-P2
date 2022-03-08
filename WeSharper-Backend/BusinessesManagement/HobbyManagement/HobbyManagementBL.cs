using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;
using System.Text.RegularExpressions;


namespace WeSharper.BusinessesManagement.Implements
{
    public class HobbyManagementBL : IHobbyManagementBL
    {
        private readonly IHobbyManagementDL _repo;

        public HobbyManagementBL(IHobbyManagementDL repo)
        {
            _repo = repo;
        }

        public Hobby AddNewHobby(Hobby h_hobby)
        {
            try
            {
                CheckDuplicateHobby(h_hobby.HobbyName);
                _repo.AddNewHobby(h_hobby);
                return h_hobby;
            }
            catch(System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
        public bool ValidHobbyName(string hobbyName)
        {
            if (Regex.IsMatch(hobbyName, @"^[a-zA-Z]*$"))
            {
                return true;
            }
            throw new Exception("Hobby name is not valid");
        }
        public List<Hobby> GetAllHobbies()
        {
            return _repo.GetAllHobbies();
        }

        public Hobby UpdateHobby(Hobby h_hobby)
        {
            return _repo.UpdateHobby(h_hobby);
        }
        public Hobby DeleteHobby(Hobby h_hobby)
        {
            return _repo.DeleteHobby(h_hobby);
        }
        public bool CheckDuplicateHobby(string hobbyName)
        {
            if( _repo.GetAllHobbies().FirstOrDefault(h => h.HobbyName.ToLower() == hobbyName.ToLower()) == null  )
            {
                return true;
            }
            else
            {
                Console.WriteLine("Failed");
                Console.WriteLine(_repo.GetAllHobbies().FirstOrDefault(h => h.HobbyName == hobbyName));
                throw new Exception(hobbyName + "Duplicate Hobby");
            }
        }
    }
}
