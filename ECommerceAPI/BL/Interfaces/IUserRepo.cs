using ECommerceAPI.BL.Models;

namespace ECommerceAPI.BL.Interfaces
{
    public interface IUserRepo : IRepo<User>
    {
        Task<bool> UserNameExistsAsync(string userName);
    }
}
