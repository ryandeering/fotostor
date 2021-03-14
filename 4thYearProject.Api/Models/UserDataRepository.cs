namespace _4thYearProject.Api.Models
{
    using _4thYearProject.Shared.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class UserDataRepository : IUserDataRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserDataRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<UserData> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public UserData GetUserDataById(string Id)
        {
            return _appDbContext.Users.FirstOrDefault(c => c.Id.Equals(Id));
        }

        public UserData GetUserDataByDisplayName(string DisplayName)
        {
            return _appDbContext.Users.Where(u => EF.Functions.Like(u.DisplayName, DisplayName)).FirstOrDefault();
        }

        public UserData AddUserData(UserData User)
        {
            var users = GetAllUsers().ToList();


            if (users.Count == 0)
            {
                var addedEntity = _appDbContext.Users.Add(User);
                Console.WriteLine(addedEntity);
                _appDbContext.SaveChanges();
                return addedEntity.Entity;
            }


            foreach (var v in users)
                if (GetUserDataById(User.Id) == null)
                {
                    var addedEntity = _appDbContext.Users.Add(User);
                    Console.WriteLine(addedEntity);
                    _appDbContext.SaveChanges();
                    return addedEntity.Entity;
                }


            return new UserData(); //filthy hack
        }

        public UserData UpdateUserData(UserData User)
        {
            var foundUserData = _appDbContext.Users.FirstOrDefault(e => e.Equals(User));

            if (foundUserData != null)
            {
                foundUserData.DisplayName = User.DisplayName;
                foundUserData.FirstName = User.FirstName;
                foundUserData.SecondName = User.SecondName;
                foundUserData.ProfilePic = User.ProfilePic;


                _appDbContext.SaveChanges();

                return foundUserData;
            }

            return null;
        }

        public void DeleteUserData(string Id)
        {
            var foundUserData = _appDbContext.Users.FirstOrDefault(u => u.Id.Equals(Id));
            if (foundUserData == null) return;

            _appDbContext.Users.Remove(foundUserData);
            _appDbContext.SaveChanges();
        }

        public UsernameList GetUserNameFromId(UsernameList list)
        {
            if (list.ListofUsernames.Any())
            {
                var Usernames = new UsernameList();

                foreach (var id in list.ListofUsernames)
                {
                    var user = _appDbContext.Users.FirstOrDefault(c => c.Id.Equals(id));
                    Usernames.ListofUsernames.Add(user.DisplayName);
                    Debug.WriteLine(user.DisplayName);
                }

                return Usernames;
            }

            return null;
        }
    }
}
