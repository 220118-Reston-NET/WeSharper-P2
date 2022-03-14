using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IProfileManagementDL
    {
        /// <summary>
        /// Adds a profile to the databse
        /// </summary>
        /// <param name="p_profile"></param>
        /// <returns></returns>
        Task<Profile> AddNewProfile(Profile p_profile);

        /// <summary>
        /// Gets all the profiles from the database
        /// </summary>
        /// <returns></returns>
        Task<List<Profile>> GetAllProfiles();

        /// <summary>
        /// Updates a profile
        /// </summary>
        /// <param name="p_profile"></param>
        /// <returns></returns>
        Task<Profile> UpdateProfile(Profile p_profile);
        Task<Profile> UpdateProfilePicture(Profile p_profile);

        Task<ApplicationUser> GetUserByUserName(string p_username);
        Task<ApplicationUser> GetUserByUserID(string p_userID);
    }
}