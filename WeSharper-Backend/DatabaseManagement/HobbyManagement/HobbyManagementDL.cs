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

        public async Task<Hobby> AddNewHobby(Hobby h_hobby)
        {
            await _context.Hobbies.AddAsync(h_hobby);
            await _context.SaveChangesAsync();

            return h_hobby;
        }

        public async Task<List<Hobby>> GetAllHobbies()
        {
            return await _context.Hobbies.ToListAsync();
        }

        public async Task<Hobby> UpdateHobby(Hobby h_hobby)
        {
            Hobby hobbyToUpdate = await _context.Hobbies.FirstOrDefaultAsync(h => h.HobbyId.ToLower() == h_hobby.HobbyId.ToLower());
            if (hobbyToUpdate != null)
            {
                hobbyToUpdate.HobbyName = h_hobby.HobbyName.ToLower();
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No profiles found");
            }
            return h_hobby;
        }

        public async Task<Hobby> DeleteHobby(Hobby h_hobby)
        {
            Hobby hobbyToRemove = await _context.Hobbies.FirstOrDefaultAsync(h => h.HobbyId.ToLower() == h_hobby.HobbyId.ToLower());
            if (hobbyToRemove != null)
            {
                _context.Hobbies.Remove(hobbyToRemove);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Profile not found. Hobby could not be deleted.");
            }
            return h_hobby;
        }
    }
}