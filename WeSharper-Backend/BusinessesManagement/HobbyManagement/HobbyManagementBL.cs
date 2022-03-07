using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

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
            return _repo.AddNewHobby(h_hobby);
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
    }
} 
