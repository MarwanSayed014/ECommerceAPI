using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL.Repos;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using ECommerceAPI.BL.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ECommerceAPI.UnitTests.ReposTests
{
    public class RepoTest
    {
        [Fact]
        public async void CreateAsync_WhenEntityIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Role entity = null;
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.CreateAsync(entity));
        }
        [Fact]
        public async void CreateAsync_WhenEntityAlreadyExists_ExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "name"
            };
            var repo = A.Fake<IRepo<Role>>();
            A.CallTo(() => repo.FindWithKeysAsync(entity)).Returns<Role>(entity);
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<Exception>(async () => await sut.CreateAsync(entity));
        }
        [Fact]
        public async void CreateAsync_WhenEntityIsNotNullAndIsNotAlreadyInDb_EntityShouldBeCreated()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var entity = new Role 
            {
                Id= Guid.NewGuid(),
                Name= "name"
            };
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.True(await sut.CreateAsync(entity));
        }
        [Fact]
        public async void UpdateAsync_WhenEntityIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Role entity = null;
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.UpdateAsync(entity));
        }
        [Fact]
        public async void UpdateAsync_WhenEntityIsNotExists_ExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "name"
            };
            var repo = A.Fake<IRepo<Role>>();
            A.CallTo(() => repo.FindWithKeysAsync(entity)).Returns<Role>(null);
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<Exception>(async () => await sut.UpdateAsync(entity));
        }
        [Fact]
        public async void UpdateAsync_WhenEntityIsNotNullAndIsAlreadyExistsInDb_EntityShouldBeUpdated()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            
            var id = Guid.NewGuid();
            var entity = new Role
            {
                Id = id,
                Name = "name"
            };

            var sut = new Repo<Role>(dbFactory);
            await sut.CreateAsync(entity);
            entity = new Role
            {
                Id = id,
                Name = "Test"
            };

            //Act//Assert
            Assert.True(await sut.UpdateAsync(entity));
        }
        [Fact]
        public async void DeleteAsync_WhenEntityIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Role entity = null;
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.DeleteAsync(entity));
        }
        [Fact]
        public async void DeleteAsync_WhenEntityIsNotExists_ExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "name"
            };
            var repo = A.Fake<IRepo<Role>>();
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<Exception>(async () => await sut.DeleteAsync(entity));
        }
        [Fact]
        public async void DeleteAsync_WhenEntityExists_EntityShouldBeDeleted()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            var entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "name"
            };
            var repo = A.Fake<IRepo<Role>>();
            var sut = new Repo<Role>(dbFactory);
            await sut.CreateAsync(entity);
            //Act//Assert
            Assert.True(await sut.DeleteAsync(entity));
        }
        [Fact]
        public async void FindAsync_WhenExpressionIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Expression<Func<Role, bool>> expression = null;
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.FindAsync(expression));
        }
        [Fact]
        public async void FindWithKeysAsync_WhenEntityIsNull_NullReferenceExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Role entity = null;
            var sut = new Repo<Role>(dbFactory);

            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.FindWithKeysAsync(entity));
        }
        [Fact]
        public async void FindWithKeysAsync_WhenEntityNotExist_ExceptionShouldBeThrown()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Role entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            var sut = new Repo<Role>(dbFactory);
            //Act//Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await sut.FindWithKeysAsync(entity));
        }
        [Fact]
        public async void FindWithKeysAsync_WhenEntityExists_EntityShouldBeReturned()
        {
            //Arrange
            var dbFactory = new DbContextFactory(Guid.NewGuid().ToString());
            Role entity = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            var sut = new Repo<Role>(dbFactory);
            sut.CreateAsync(entity);
            //Act
            var result = await sut.FindWithKeysAsync(entity);
            //Assert
            Assert.NotNull(result);
        }

    }
}
