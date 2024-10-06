using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.UnitTests.ReposTests
{
    public class UserRoleRepoTest
    {
        [Fact]
        public async void UserIsInRoleAsync_WhenUserIsInRole_TrueShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var roleId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var role = new Role
            {
                Id = roleId,
                Name = "Role"
            };

            var user = new User
            {
                Id = userId,
                Password= "password",
                Name= "name",
                ProfileImgPath = "dasd",
                Gender = BL.Types.GenderTypes.Male
            };

            var userRole = new UserRole
            {
                RoleId = roleId,
                UserId= userId
            };
            var roleRepo = new RoleRepo(dbFactory);
            var userRepo = new UserRepo(dbFactory);
            await roleRepo.CreateAsync(role);
            await userRepo.CreateAsync(user);
            var sut = new UserRoleRepo(dbFactory);
            await sut.CreateAsync(userRole);

            //Act//Assert
            Assert.True(await sut.UserIsInRoleAsync(userId, roleId));
        }
        [Fact]
        public async void UserIsInRoleAsync_WhenUserIsNotInRole_FalseShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var roleId = new Guid();
            var userId = new Guid();

            var sut = new UserRoleRepo(dbFactory);

            //Act//Assert
            Assert.False(await sut.UserIsInRoleAsync(userId, roleId));
        }
    }
}
