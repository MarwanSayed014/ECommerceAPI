using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.DAL.Repos
{
    public class RoleRepo : Repo<Role>, IRoleRepo
    {
        public RoleRepo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

       
        public async Task<bool> RoleNameExistsAsync(string roleName)
        {
            try
            {
                if(roleName == null)
                    throw new NullReferenceException("RoleName should not be null");
                return (await FindAsync(x => x.Name == roleName)).Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Role> GetRoleAsync(string roleName)
        {
            try
            {
                if (roleName == null)
                    throw new NullReferenceException("RoleName should not be null");
                return (await FindAsync(x => x.Name == roleName)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Role>> GetRolesAsync(IEnumerable<Guid> roleIds)
        {
            try
            {
                var roles = new List<Role>();
                foreach (var id in roleIds)
                {
                    roles.Add((await FindAsync(x => x.Id == id)).SingleOrDefault());
                }
                return roles;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
