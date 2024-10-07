using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.BL.Types;

namespace ECommerceAPI.Services.Authentication
{
    public class UserEventHandler
    {
        public IRoleRepo _roleRepo { get; }
        public IUserRoleRepo _userRoleRepo { get; }
        public ICartRepo _cartRepo { get; }
        public UserEventHandler(IRoleRepo roleRepo,
            IUserRoleRepo userRoleRepo, ICartRepo cartRepo)
        {
            _roleRepo = roleRepo;
            _userRoleRepo = userRoleRepo;
            _cartRepo = cartRepo;
        }
        public async Task AddUserToUserRole(User user)
        {
            var userRole = new UserRole
            {
                RoleId = (Guid)((await _roleRepo.GetRoleAsync(RoleTypes.User))?.Id),
                UserId = user.Id
            };
            await _userRoleRepo.CreateAsync(userRole);
        }
        public async Task CreateUserCart(User user)
        {
            await _cartRepo.CreateAsync(new Cart
            {
                CartId = Guid.NewGuid(),
                UserId = user.Id
            });
        }
    }
}
