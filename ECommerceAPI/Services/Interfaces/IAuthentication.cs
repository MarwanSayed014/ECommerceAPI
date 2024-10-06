using ECommerceAPI.BL.ViewModels;

namespace ECommerceAPI.Services.Interfaces
{
    public interface IAuthentication
    {
        Task<RegistrationMassages> RegisterAsync(RegisterViewModel model);
        Task<string> LoginAsync(LoginViewModel model);
    }
}
