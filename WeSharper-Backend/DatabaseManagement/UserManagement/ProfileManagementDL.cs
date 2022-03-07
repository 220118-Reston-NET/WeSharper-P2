using Microsoft.EntityFrameworkCore;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Implements
{
    public class ProfileManagementDL : IProfileManagementDL
    {
        private readonly WeSharperContext _context;

        public ProfileManagementDL(WeSharperContext context)
        {
            _context = context;
        }

        public Profile AddNewProfile(Profile p_profile)
        {
            p_profile.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            _context.Profiles.Add(p_profile);
            _context.SaveChanges();

            return p_profile;
        }
        public List<Profile> GetAllProfiles()
        {
            return _context.Profiles.ToList();
        }
        public Profile GetAProfile(string userId)
        {
            Profile profToUpdate = _context.Profiles.FirstOrDefault(p => p.UserId == userId);
            if(profToUpdate != null)
            {
                return profToUpdate;
            }
            else
            {
                throw new Exception("Profile not found");
            }
            
        }
        public Profile UpdateProfile(Profile p_profile)
        {
            Profile profToUpdate = _context.Profiles.FirstOrDefault(p => p.UserId == p_profile.UserId);
            if(profToUpdate != null)
            {
                profToUpdate.FirstName = p_profile.FirstName;
                profToUpdate.LastName = p_profile.LastName;
                profToUpdate.ProfilePictureUrl = p_profile.ProfilePictureUrl;
                profToUpdate.Bio = p_profile.Bio;
                _context.SaveChanges();
            }
            else{
                throw new Exception("No profiles found");
            }
            return p_profile;
        }
    }
}