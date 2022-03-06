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
    }
}