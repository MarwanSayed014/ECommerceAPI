using ECommerceAPI.Helpers.Interfaces;
using System.Text;
using XSystem.Security.Cryptography;

namespace ECommerceAPI.Helpers
{
    public class PasswordManager : IPasswordManager
    {
        public async Task<string> Hash(string password)
        {
			try
			{
				using (var sha256 = new SHA256Managed())
				{
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                    return Convert.ToBase64String(hashedBytes);
                }
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
