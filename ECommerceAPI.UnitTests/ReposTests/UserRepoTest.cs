using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.UnitTests.ReposTests
{
    public class UserRepoTest
    {
        [Fact]
        public async void UserNameExistsAsync_WhenUserNameIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            string userName = null;
            var sut = new UserRepo(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.UserNameExistsAsync(userName));
        }
        [Fact]
        public async void UserNameExistsAsync_WhenUserNameAlreadyExists_TrueShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var userName = "Test";
            var sut = new UserRepo(dbFactory);
            sut.CreateAsync(new User
            {
                Id = Guid.NewGuid(),
                Name = userName,
                Password = "adad"
            });

            //Act//Assert
            Assert.True(await sut.UserNameExistsAsync(userName));
        }
        [Fact]
        public async void UserNameExistsAsync_WhenUserNameIsNotExist_FalseShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var userName = "Test";
            var sut = new UserRepo(dbFactory);

            //Act//Assert
            Assert.False(await sut.UserNameExistsAsync(userName));
        }
    }
}
