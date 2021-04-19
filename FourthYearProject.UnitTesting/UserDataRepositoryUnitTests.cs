using System;
using System.Collections.Generic;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class UserDataRepositoryUnitTests
    {
        static readonly Dictionary<string, string> inMemorySettings = new Dictionary<string, string>
        {
            {"TopLevelKey", "TopLevelValue"},
            {"SectionName:SomeKey", "SectionValue"},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        [Fact]
        public void GetUserDataById_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Users.Add(test1);
            context.SaveChanges();

        
            var repo = new UserDataRepository(context, configuration);
            var userData = repo.GetUserDataInFull(test1.Id);

            Assert.Equal(test1.Address.UserAddress, userData.Address.UserAddress);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public void GetUserDataInFull_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Users.Add(test1);
            context.SaveChanges();
            var repo = new UserDataRepository(context, configuration);
            var userData = repo.GetUserDataById(test1.Id);

            Assert.Equal(test1.Address.UserAddress, userData.Address.UserAddress);
            context.ChangeTracker.Clear();
        }



        [Fact]
        public void AddUserData_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new UserDataRepository(context, configuration);
            repo.AddUserData(test1);
            var userData = repo.GetUserDataById(test1.Id);

            Assert.Equal(test1.DisplayName, userData.DisplayName);
            context.ChangeTracker.Clear();

        }


        [Fact]
        public void AddUserDataMoreThanOne_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test2 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new UserDataRepository(context, configuration);
            repo.AddUserData(test1);
            repo.AddUserData(test2);
            var userData = repo.GetUserDataById(test2.Id);

            Assert.Equal(test2.DisplayName, test2.DisplayName);
            context.ChangeTracker.Clear();

        }

        [Fact]
        public void UpdateUserData_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new UserDataRepository(context, configuration);
            repo.AddUserData(test1);
            var userData = repo.GetUserDataById(test1.Id);
            userData.DisplayName = "dumdum";
            userData = repo.UpdateUserData(userData);

            Assert.Equal("dumdum", userData.DisplayName);
            context.ChangeTracker.Clear();

        }

        [Fact]
        public void UpdateUserData_FAILTest()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test2 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new UserDataRepository(context, configuration);
            repo.AddUserData(test1);
            var userData = repo.GetUserDataById(test1.Id);
            userData.DisplayName = "dumdum";
            userData = repo.UpdateUserData(test2);

            Assert.Null(userData);
            context.ChangeTracker.Clear();

        }


        [Fact]
        public void DeleteUserData_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            var repo = new UserDataRepository(context, configuration);
            repo.AddUserData(test1);
            repo.DeleteUserData(test1.Id);
            var userData = repo.GetUserDataById(test1.Id);
            Assert.Null(userData);

            context.ChangeTracker.Clear();

        }


        [Fact]
        public void GetUserDataByDisplayName_Test()
        {
            var test1 = GenFu.GenFu.New<UserData>();
            var test1Address = GenFu.GenFu.New<Address>();
            test1.Address = test1Address;

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Users.Add(test1);
            context.SaveChanges();
            var repo = new UserDataRepository(context, configuration);
            var userData = repo.GetUserDataByDisplayName(test1.DisplayName);

            Assert.Equal(test1.Bio, userData.Bio);
            context.ChangeTracker.Clear();
        }


        [Fact]
        public void GetAllUserDatasTest()
        {
            var test1 = GenFu.GenFu.New<UserData>();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            context.Users.Add(test1);
            context.SaveChanges();
            var repo = new UserDataRepository(context, configuration);
            var UserData = repo.GetAllUsers();

            Assert.Equal(UserData.First().Email, test1.Email);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public void DeleteUserData()
        {
            var i = 20;
            GenFu.GenFu.Configure<UserData>()
                .Fill(ud => ud.Id, () => i++.ToString());

            var test1 = GenFu.GenFu.ListOf<UserData>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            foreach (var userData in test1)
            {
                context.Users.Add(userData);
            }
            context.SaveChanges();
            var repo = new UserDataRepository(context, configuration);
            repo.DeleteUserData(test1.FirstOrDefault(ud => ud.Id == "3")?.Id);
            test1.Remove(test1.FirstOrDefault(ud => ud.Id == "3"));

            var users = context.Users.ToList();
            for (int j = 20; j < test1.Count; j++)
            {
                Assert.Equal(test1[j].FirstName, users[j].FirstName);

            }
            context.ChangeTracker.Clear();

        }

        [Fact]
        public void GetUserNameFromId()
        {
            var i = 1;
            GenFu.GenFu.Configure<UserData>()
                .Fill(ud => ud.Id, () => i++.ToString());

            var test1 = GenFu.GenFu.ListOf<UserData>(3);

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new AppDbContext(options);
            foreach (var userData in test1)
            {
                context.Users.Add(userData);
            }

            context.SaveChanges();

            var repo = new UserDataRepository(context, configuration);

            foreach (var userData in test1)
            {
                var ProfileData = new FeedProfileData
                {
                    Username = userData.DisplayName,
                    ProfilePicURL = userData.ProfilePic,
                    FName = userData.FirstName,
                    LName = userData.SecondName
                };

                Assert.Equal(ProfileData.Username, repo.GetUserNameFromId(userData.Id).Username);
            }
            context.ChangeTracker.Clear();
        }


    }
}