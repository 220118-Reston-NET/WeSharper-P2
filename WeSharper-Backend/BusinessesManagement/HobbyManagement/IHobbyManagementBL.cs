using WeSharper.Models;

namespace WeSharper.BusinessesManagement.Interfaces
{
    public interface IHobbyManagementBL
    {
        Hobby AddNewHobby(Hobby h_hobby);
        List<Hobby> GetAllHobbies();
        Hobby UpdateHobby(Hobby h_hobby);
        Hobby DeleteHobby(Hobby h_hobby);
    }
}