using ECommerceAPI.BL.Models;

namespace ECommerceAPI.BL.Interfaces
{
    public interface IRoleRepo : IRepo<Role>
    {
        Task<bool> RoleNameExistsAsync(string roleName);
        Task<Role> GetRoleAsync(string roleName);
        Task<IEnumerable<Role>> GetRolesAsync(IEnumerable<Guid> roleIds);
    }
}
