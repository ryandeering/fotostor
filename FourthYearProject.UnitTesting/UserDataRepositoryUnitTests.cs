using System;
using _4thYearProject.Api.Models;
using _4thYearProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace FourthYearProject.UnitTesting
{
    public class UserDataRepositoryUnitTests
    {
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
            var repo = new UserDataRepository(context);
            var UserData = repo.GetAllUsers();

            Assert.Equal(UserData.First().Email, test1.Email);
        }

        [Fact]
        public void DeleteUserData()
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
            var repo = new UserDataRepository(context);
            repo.DeleteUserData(test1.FirstOrDefault(ud => ud.Id == "3")?.Id);
            test1.Remove(test1.FirstOrDefault(ud => ud.Id == "3"));

            var users = context.Users.ToList();
            for (int j = 0; j < test1.Count; j++)
            {
                Assert.Equal(test1[j].FirstName, users[j].FirstName);

            }

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

            var repo = new UserDataRepository(context);

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
        }


        [Fact]
        public void GetUserDataByDisplayName()
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

            var repo = new UserDataRepository(context);

            for (int j = 0; j < test1.Count; j++)
            {

                Assert.Equal(test1[j].FirstName, repo.GetUserDataByDisplayName(test1[j].DisplayName).FirstName);
            }

        }

    }
}