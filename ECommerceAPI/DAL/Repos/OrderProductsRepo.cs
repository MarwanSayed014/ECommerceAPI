using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace ECommerceAPI.DAL.Repos
{
    public class OrderProductsRepo : Repo<OrderProducts>, IOrderProductsRepo
    {
        public OrderProductsRepo(IDesignTimeDbContextFactory<ApplicationDbContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }
}
