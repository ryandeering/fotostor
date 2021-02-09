using _4thYearProject.Shared.Models;
using System.Collections.Generic;

namespace _4thYearProject.Api.Models
{
    public interface IUserDataRepository
    {
        IEnumerable<UserData> GetAllUsers();
        UserData GetUserDataById(string Id);
        UserData GetUserDataByDisplayName(string DisplayName);
        UserData AddUserData(UserData User);
        UserData UpdateUserData(UserData User);
        void DeleteUserData(string Id);
    }
}
