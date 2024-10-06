using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.DAL.Repos
{
    public class UserRoleRepo : Repo<UserRole> , IUserRoleRepo
    {
        public UserRoleRepo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<IEnumerable<UserRole>> GetUserRoles(Guid userId)
        {
            return (await FindAsync(x=> x.UserId == userId)).ToList();
        }

        public async Task<bool> UserIsInRoleAsync(Guid userId, Guid roleId)
        {
            try
            {
                return (await FindAsync(x => x.UserId == userId && x.RoleId == roleId)).Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
