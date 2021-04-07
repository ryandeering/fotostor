using _4thYearProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _4thYearProject.Server.Services
{
    public interface IUserDataService
    {
        Task<IEnumerable<UserData>> GetAllUsers();
        Task<UserData> GetUserDataDetails(string Id);
        Task<UserData> GetUserDataDetailsByDisplayName(string DisplayName);
        Task<UserData> AddUserData(UserData User);
        Task UpdateUserData(UserData User);
        Task DeleteUserData(string Id);
        Task<UserData> GetUserDataDetailsInFull(string Id);
        Task<FeedProfileData> GetUserNameFromId(string id);
    }
}

