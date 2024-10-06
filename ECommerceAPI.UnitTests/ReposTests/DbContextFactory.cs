using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.UnitTests.ReposTests
{
    internal class DbContextFactory : IDesignTimeDbContextFactory<InMemoryDbContext>
    {
        public string _dbName { get; }
        public DbContextFactory(string dbName)
        {
            _dbName = dbName;
        }

        

        public InMemoryDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder().Options;
            return new InMemoryDbContext(options, _dbName);
        }
    }
}
