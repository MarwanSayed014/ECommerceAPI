using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.DAL.Repos
{
    public class CategoryRepo : Repo<Category>, ICategoryRepo
    {
        public CategoryRepo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
