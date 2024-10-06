using ECommerceAPI.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.UnitTests.ReposTests
{
    internal class InMemoryDbContext : ApplicationDbContext
    {
        public string _dbName { get; }

        public InMemoryDbContext(DbContextOptions options, string dbName) : base(options)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_dbName);
        }

        //public override void Dispose()
        //{
        //    Database.EnsureDeleted();
        //    base.Dispose();
        //}
    }
}
