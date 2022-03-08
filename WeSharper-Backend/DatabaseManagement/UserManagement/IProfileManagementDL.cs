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
        Profile AddNewProfile(Profile p_profile);
        /// <summary>
        /// Gets all the profiles from the database
        /// </summary>
        /// <returns></returns>
        List<Profile> GetAllProfiles();
        /// <summary>
        /// Updates a profile
        /// </summary>
        /// <param name="p_profile"></param>
        /// <returns></returns>
        Profile UpdateProfile(Profile p_profile);
    }
}