using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class HobbyManagementDL : IHobbyManagementDL
    {
        private readonly WeSharperContext _context;

        public HobbyManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Hobby AddNewHobby(Hobby h_hobby)
        {
            h_hobby.HobbyName.ToLower();
            _context.Hobbies.Add(h_hobby);
            _context.SaveChanges();

            return h_hobby;
        }

        public List<Hobby> GetAllHobbies()
        {
            return _context.Hobbies.ToList();
        }

        public Hobby UpdateHobby(Hobby h_hobby)
        {
            Hobby hobbyToUpdate = _context.Hobbies.FirstOrDefault(h => h.HobbyId.ToLower() == h_hobby.HobbyId.ToLower());
            if (hobbyToUpdate != null)
            {
                hobbyToUpdate.HobbyName = h_hobby.HobbyName.ToLower();
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No profiles found");
            }
            return h_hobby;
        }

        public Hobby DeleteHobby(Hobby h_hobby)
        {
            Hobby hobbyToRemove = _context.Hobbies.FirstOrDefault(h => h.HobbyId.ToLower() == h_hobby.HobbyId.ToLower());
            if (hobbyToRemove != null)
            {
                _context.Hobbies.Remove(hobbyToRemove);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Profile not found. Hobby could not be deleted.");
            }
            return h_hobby;
        }
    }
}