using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.DAL.Repos
{
    public class BrandRepo : Repo<Brand>, IBrandRepo
    {
        public BrandRepo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
