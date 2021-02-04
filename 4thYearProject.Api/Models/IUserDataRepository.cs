using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _4thYearProject.Shared.Models;

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
