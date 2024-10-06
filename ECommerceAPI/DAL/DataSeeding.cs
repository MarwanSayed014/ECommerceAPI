using ECommerceAPI.BL.Models;
using ECommerceAPI.BL.Types;
using ECommerceAPI.DAL.Repos;

namespace ECommerceAPI.DAL
{
    public class DataSeeding
    {
        public RoleRepo _roleRepo { get; }
        public DataSeeding()
        {
            _roleRepo = new RoleRepo(new DesignTimeDbContextFactory());
        }
        public async Task SeedAsync()
        {
            try
            {
                if (!(await _roleRepo.RoleNameExistsAsync(RoleTypes.User)))
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = RoleTypes.User
                    };
                    await _roleRepo.CreateAsync(role);
                }
                if (!(await _roleRepo.RoleNameExistsAsync(RoleTypes.Admin)))
                {
                    var role = new Role
                    {
                        Id = Guid.NewGuid(),
                        Name = RoleTypes.Admin
                    };
                    await _roleRepo.CreateAsync(role);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
