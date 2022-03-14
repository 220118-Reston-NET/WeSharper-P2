using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IProfileManagementBL
    {
        /// <summary>
        /// Adds a profile to the database
        /// </summary>
        /// <param name="p_profile"></param>
        /// <returns></returns>
        Task<Profile> AddNewProfile(Profile p_profile);
        /// <summary>
        /// Gets all of the profiles from the database
        /// </summary>
        /// <returns></returns>
        Task<List<Profile>> GetAllProfiles();
        /// <summary>
        /// Gets a profile from the database
        /// </summary>
        /// <returns></returns>
        Task<Profile> GetAProfile(string userId);
        /// <summary>
        /// Updates the information of the profile in the database
        /// </summary>
        /// <param name="p_profile"></param>
        /// <returns></returns>
        Task<Profile> UpdateProfile(Profile p_profile);

        Task<Profile> UpdateProfilePicture(Profile p_profile);

        Task<ApplicationUser> GetUserByUserName(string p_username);
        Task<ApplicationUser> GetUserByUserID(string p_userID);
    }
}
