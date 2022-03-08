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

        public Profile AddNewProfile(Profile p_profile)
        {
            return _repo.AddNewProfile(p_profile);
        }
        public List<Profile> GetAllProfiles()
        {
            return _repo.GetAllProfiles();
        }
        public Profile GetAProfile(string userId)
        {
            Profile profile = _repo.GetAllProfiles().FirstOrDefault(p => p.UserId == userId);
            if (profile != null)
            {
                return profile;
            }
            else
            {
                throw new Exception("Profile not found");
            }
        }
        public Profile UpdateProfile(Profile p_profile)
        {
            return _repo.UpdateProfile(p_profile);
        }
    }
}


