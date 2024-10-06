using ECommerceAPI.BL.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>().HasKey(x=> new { x.UserId, x.RoleId });
            modelBuilder.Entity<CartProducts>().HasKey(x=> new { x.CartId, x.ProductId });
            modelBuilder.Entity<Cart>().HasKey(x=> new { x.CartId, x.UserId });
            modelBuilder.Entity<OrderProducts>().HasKey(x=> new { x.OrderId, x.ProductId });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<CartProducts> CartProducts { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Cart> Cart { get; set; }
    }
}
