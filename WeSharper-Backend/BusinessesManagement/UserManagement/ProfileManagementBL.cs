using WeSharper.BusinessesManagement.Interfaces;
using WeSharper.DatabaseManagement.Interfaces;
using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Implements
{
    public class ProfileManagementBL : IProfileManagementBL
    {
        private readonly IProfileManagementDL _repo;

        public ProfileManagementBL(IProfileManagementDL repo)
        {
            _repo = repo;
        }

        public async Task<Profile> AddNewProfile(Profile p_profile)
        {
            return await _repo.AddNewProfile(p_profile);
        }
        public async Task<List<Profile>> GetAllProfiles()
        {
            return await _repo.GetAllProfiles();
        }
        public async Task<Profile> GetAProfile(string userId)
        {
            List<Profile> _result = await _repo.GetAllProfiles();
            return _result.FirstOrDefault(p => p.UserId.Equals(userId));
        }

        public async Task<ApplicationUser> GetUserByUserID(string p_userID)
        {
            return await _repo.GetUserByUserID(p_userID);
        }

        public async Task<ApplicationUser> GetUserByUserName(string p_username)
        {
            return await _repo.GetUserByUserName(p_username);
        }

        public async Task<Profile> UpdateProfile(Profile p_profile)
        {
            return await _repo.UpdateProfile(p_profile);
        }

        public async Task<Profile> UpdateProfilePicture(Profile p_profile)
        {
            return await _repo.UpdateProfilePicture(p_profile);
        }
    }
}


