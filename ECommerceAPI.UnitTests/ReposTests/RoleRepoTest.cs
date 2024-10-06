using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL.Repos;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.UnitTests.ReposTests
{
    public class RoleRepoTest
    {
        [Fact]
        public async void RoleNameExistsAsync_WhenRoleNameIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            string roleName = null;
            var sut = new RoleRepo(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.RoleNameExistsAsync(roleName));
        }
        [Fact]
        public async void RoleNameExistsAsync_WhenRoleNameAlreadyExists_TrueShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var roleName = "Test";
            var sut = new RoleRepo(dbFactory);
            sut.CreateAsync(new Role
            {
                Id = Guid.NewGuid(),
                Name = roleName
            });

            //Act//Assert
            Assert.True(await sut.RoleNameExistsAsync(roleName));
        }
        [Fact]
        public async void RoleNameExistsAsync_WhenRoleNameIsNotExist_FalseShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var roleName = "Test";
            var sut = new RoleRepo(dbFactory);

            //Act//Assert
            Assert.False(await sut.RoleNameExistsAsync(roleName));
        }
    }
}
