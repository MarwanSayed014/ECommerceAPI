using ECommerceAPI.BL.ViewModels;
using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL.Repos;
using ECommerceAPI.Services.Interfaces;
using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.Helpers.Interfaces;
using ECommerceAPI.Helpers;
using ECommerceAPI.BL.Types;

namespace ECommerceAPI.Services.Authentication
{
    public class Authentication : IAuthentication
    {
        public IUserRepo _userRepo { get; }
        public IRoleRepo _roleRepo { get; }
        public IUserRoleRepo _userRoleRepo { get; }
        public IPasswordManager _passwordManager { get; }
        public JWTHelper _jWTHelper { get; }

        public Authentication(IUserRepo userRepo, IRoleRepo roleRepo,
            IUserRoleRepo userRoleRepo, IPasswordManager passwordManager,
            JWTHelper jWTHelper)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _userRoleRepo = userRoleRepo;
            _passwordManager= passwordManager;
            _jWTHelper = jWTHelper;
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
                    if(await _userRepo.CreateAsync(user))
                    {
                        var userRole = new UserRole
                        {
                            RoleId = (Guid)((await _roleRepo.GetRoleAsync(RoleTypes.User))?.Id),
                            UserId = user.Id
                        };
                        await _userRoleRepo.CreateAsync(userRole);
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
