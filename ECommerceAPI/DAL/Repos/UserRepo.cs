using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore.Design;
using static ECommerceAPI.BL.Interfaces.IUserRepo;

namespace ECommerceAPI.DAL.Repos
{
    public class UserRepo : Repo<User>, IUserRepo
    {
        public event UserCreatedEventHandler UserCreated;

        public UserRepo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            try
            {
                if (userName == null)
                    throw new NullReferenceException("UserName should not be null");
                return (await FindAsync(x => x.Name == userName)).Count() > 0 ? true : false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> CreateAsync(User user)
        {
            var result = await base.CreateAsync(user);
            if (result == true)
            {
                UserCreated?.Invoke(user);
            }
            return result;
        }
    }
}
