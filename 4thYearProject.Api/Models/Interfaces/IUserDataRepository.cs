﻿using System.Collections.Generic;
using _4thYearProject.Shared.Models;

namespace _4thYearProject.Api.Models
{
    public interface IUserDataRepository
    {
        IEnumerable<UserData> GetAllUsers();

        UserData GetUserDataById(string Id);

        UserData GetUserDataByDisplayName(string DisplayName);
        UserData GetUserDataInFull(string Id);

        UserData AddUserData(UserData User);

        UserData UpdateUserData(UserData User);

        void DeleteUserData(string Id);

        FeedProfileData GetUserNameFromId(string UserId);
    }
}