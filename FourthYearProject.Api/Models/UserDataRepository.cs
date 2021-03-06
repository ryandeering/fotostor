using System;
using System.Collections.Generic;
using System.Linq;
using FourthYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FourthYearProject.Api.Models
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public UserDataRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public IEnumerable<UserData> GetAllUsers()
        {
            return _appDbContext.Users;
        }

        public UserData GetUserDataById(string Id)
        {
            return _appDbContext.Users.FirstOrDefault(c => c.Id.Equals(Id));
        }

        public UserData GetUserDataInFull(string Id)
        {
            return _appDbContext.Users.Include("Address").FirstOrDefault(c => c.Id.Equals(Id));
        }

        public UserData GetUserDataByDisplayName(string DisplayName)
        {
            return _appDbContext.Users.FirstOrDefault(u => EF.Functions.Like(u.DisplayName, DisplayName));
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


            return new UserData();
        }

        public UserData UpdateUserData(UserData User)
        {
            var foundUserData = _appDbContext.Users.FirstOrDefault(e => e.Equals(User));

            if (foundUserData == null) return null;
            foundUserData.DisplayName = User.DisplayName;
            foundUserData.FirstName = User.FirstName;
            foundUserData.SecondName = User.SecondName;
            foundUserData.ProfilePic = User.ProfilePic;
            foundUserData.Address = User.Address;
            foundUserData.Bio = User.Bio;


            _appDbContext.SaveChanges();

            return foundUserData;
        }


        public FeedProfileData GetUserNameFromId(string UserId)
        {
            var _optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _optionsBuilder.UseSqlServer(_connectionString);
            using var context = new AppDbContext(_optionsBuilder.Options);
            var User = context.Users.FirstOrDefault(c => c.Id.Equals(UserId));

            var ProfileData = new FeedProfileData
            {
                Username = User.DisplayName,
                ProfilePicURL = User.ProfilePic,
                FName = User.FirstName,
                LName = User.SecondName
            };
            return ProfileData;
        }

        public FeedProfileData GetUserNameFromIdAlt(string UserId)
        {
            var User = _appDbContext.Users.AsNoTracking().FirstOrDefault(c => c.Id.Equals(UserId));

            var ProfileData = new FeedProfileData
            {
                Username = User.DisplayName,
                ProfilePicURL = User.ProfilePic,
                FName = User.FirstName,
                LName = User.SecondName
            };
            return ProfileData;
        }
    }
}