using ECommerceAPI.BL.Models;

namespace ECommerceAPI.BL.Interfaces
{
    public interface IUserRepo : IRepo<User>
    {
        public delegate Task UserCreatedEventHandler(User user);
        public event UserCreatedEventHandler UserCreated;
        Task<bool> UserNameExistsAsync(string userName);
    }
}
