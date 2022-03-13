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
            return _repo.GetAllProfiles().FirstOrDefault(p => p.UserId.Equals(userId));
        }

        public ApplicationUser GetUserByUserID(string p_userID)
        {
            return _repo.GetUserByUserID(p_userID);
        }

        public ApplicationUser GetUserByUserName(string p_username)
        {
            return _repo.GetUserByUserName(p_username);
        }

        public Profile UpdateProfile(Profile p_profile)
        {
            return _repo.UpdateProfile(p_profile);
        }

        public Profile UpdateProfilePicture(Profile p_profile)
        {
            return _repo.UpdateProfilePicture(p_profile);
        }
    }
}


