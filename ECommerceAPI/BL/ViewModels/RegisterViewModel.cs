using ECommerceAPI.BL.Types;

namespace ECommerceAPI.BL.ViewModels
{
    public class RegisterViewModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required GenderTypes Gender { get; set; }
    }
}
