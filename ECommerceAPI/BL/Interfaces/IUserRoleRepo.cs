using ECommerceAPI.BL.Models;

namespace ECommerceAPI.BL.Interfaces
{
    public interface IUserRoleRepo : IRepo<UserRole>
    {
        Task<bool> UserIsInRoleAsync(Guid userId, Guid roleId);
        Task<IEnumerable<UserRole>> GetUserRoles(Guid userId);
    }
}
