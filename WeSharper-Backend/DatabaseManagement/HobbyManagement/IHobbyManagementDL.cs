using WeSharper.Models;

namespace WeSharper.DatabaseManagement.Interfaces
{
    public interface IHobbyManagementDL
    {
        /// <summary>
        /// Adds the hobby into the database
        /// </summary>
        /// <param name="h_hobby"></param>
        /// <returns></returns>
        Task<Hobby> AddNewHobby(Hobby h_hobby);
        /// <summary>
        /// Retrieves all of the hobbies in a list format
        /// </summary>
        /// <returns></returns>
        Task<List<Hobby>> GetAllHobbies();
        /// <summary>
        /// Updates the information of the hobby in the database
        /// </summary>
        /// <param name="h_hobby"></param>
        /// <returns></returns>
        Task<Hobby> UpdateHobby(Hobby h_hobby);
        /// <summary>
        /// Deletes a hobby in the database
        /// </summary>
        /// <param name="h_hobby"></param>
        /// <returns></returns>
        Task<Hobby> DeleteHobby(Hobby h_hobby);
    }
}