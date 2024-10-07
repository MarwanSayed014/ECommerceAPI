using ECommerceAPI.BL.ViewModels;
using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL.Repos;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.Helpers.Interfaces;
using ECommerceAPI.Helpers;
using ECommerceAPI.BL.Types;
using static ECommerceAPI.BL.Interfaces.IUserRepo;

namespace ECommerceAPI.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        public IUserRepo _userRepo { get; }
        public IRoleRepo _roleRepo { get; }
        public IUserRoleRepo _userRoleRepo { get; }
        public ICartRepo _cartRepo { get; }
        public IPasswordManager _passwordManager { get; }
        public JWTHelper _jWTHelper { get; }
        public UserEventHandler _userEventHandler { get; }

        public Authentication(IUserRepo userRepo, IRoleRepo roleRepo,
            IUserRoleRepo userRoleRepo, ICartRepo cartRepo, IPasswordManager passwordManager,
            JWTHelper jWTHelper, UserEventHandler userEventHandler)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _userRoleRepo = userRoleRepo;
            _cartRepo = cartRepo;
            _passwordManager = passwordManager;
            _jWTHelper = jWTHelper;
            _userEventHandler = userEventHandler;
        }

        

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            try
            {
                var hashedPassword = await _passwordManager.Hash(model.Password);
                var user = (await _userRepo.FindAsync(x=> x.Name == model.UserName && 
                           x.Password == hashedPassword)).SingleOrDefault();
                if (user == null)
                    return null;
                var roleIds = (await _userRoleRepo.GetUserRoles(user.Id)).Select(x=> x.RoleId);
                var roles = await _roleRepo.GetRolesAsync(roleIds);
                return await _jWTHelper.GenerateAcessToken(await _jWTHelper.GenerateUserClaims(user, roles));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<RegistrationMassages> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                if (await IsRegisterModelValidAsync(model) == RegistrationMassages.UserNameNotExists)
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Password= await _passwordManager.Hash(model.Password),
                        Gender= model.Gender,
                        Name = model.UserName
                    };
                    _userRepo.UserCreated += _userEventHandler.AddUserToUserRole;
                    _userRepo.UserCreated += _userEventHandler.CreateUserCart;
                    if (await _userRepo.CreateAsync(user))
                    {
                        return RegistrationMassages.Succeeded;
                    }
                    else
                        return RegistrationMassages.Failed;
                    
                }
                return RegistrationMassages.UserNameAlreadyExists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private async Task<RegistrationMassages> IsRegisterModelValidAsync(RegisterViewModel model)
        {
            try
            {
                if (await _userRepo.UserNameExistsAsync(model.UserName))
                    return RegistrationMassages.UserNameAlreadyExists;
                return RegistrationMassages.UserNameNotExists;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
