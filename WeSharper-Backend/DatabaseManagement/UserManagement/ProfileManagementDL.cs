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

        public async Task<Profile> AddNewProfile(Profile p_profile)
        {
            p_profile.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            await _context.Profiles.AddAsync(p_profile);
            await _context.SaveChangesAsync();

            return p_profile;
        }

        public async Task<List<Profile>> GetAllProfiles()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByUserID(string p_userID)
        {
            return await _context.Users.FindAsync(p_userID);
        }

        public async Task<ApplicationUser> GetUserByUserName(string p_username)
        {
            return await _context.Users.SingleOrDefaultAsync(p => p.UserName.Equals(p_username));
        }

        public async Task<Profile> UpdateProfile(Profile p_profile)
        {
            Profile profToUpdate = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == p_profile.UserId);
            if (profToUpdate != null)
            {
                profToUpdate.FirstName = p_profile.FirstName;
                profToUpdate.LastName = p_profile.LastName;
                profToUpdate.Bio = p_profile.Bio;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No profiles found");
            }
            return p_profile;
        }

        public async Task<Profile> UpdateProfilePicture(Profile p_profile)
        {
            Profile _profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == p_profile.UserId);
            if (_profile != null)
            {
                _profile.ProfilePictureUrl = p_profile.ProfilePictureUrl;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No profiles found");
            }

            return _profile;
        }
    }
}