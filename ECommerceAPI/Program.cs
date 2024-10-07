
using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.DAL;
using ECommerceAPI.DAL.Repos;
using ECommerceAPI.Helpers;
using ECommerceAPI.Services.Authentication;
using ECommerceAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //DB Connection
            builder.Services.AddDbContext<ApplicationDbContext>(cfg => cfg.UseSqlServer(
                builder.Configuration["ConnectionStrings:DefaultConnection"]));

            var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
            builder.Services.AddSingleton(jwtOptions);
            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
                    };
                });

            builder.Services.AddScoped<IAuthentication, Authentication>(x=> new Authentication(
                    new UserRepo(new DesignTimeDbContextFactory()),
                    new RoleRepo(new DesignTimeDbContextFactory()),
                    new UserRoleRepo(new DesignTimeDbContextFactory()),
                    new CartRepo(new DesignTimeDbContextFactory()),
                    new PasswordManager(),
                    new JWTHelper(jwtOptions),
                    new UserEventHandler(new RoleRepo(new DesignTimeDbContextFactory()),
                    new UserRoleRepo(new DesignTimeDbContextFactory()),
                    new CartRepo(new DesignTimeDbContextFactory()))
                )
            );
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>(x => new CategoryRepo(new DesignTimeDbContextFactory()));
            builder.Services.AddScoped<IBrandRepo, BrandRepo>(x => new BrandRepo(new DesignTimeDbContextFactory()));
            builder.Services.AddScoped<IProductRepo, ProductRepo>(x => new ProductRepo(new DesignTimeDbContextFactory()));
            builder.Services.AddScoped<ICartProductsRepo, CartProductsRepo>(x => new CartProductsRepo(new DesignTimeDbContextFactory()));
            builder.Services.AddScoped<ICartRepo, CartRepo>(x => new CartRepo(new DesignTimeDbContextFactory()));

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            new DataSeeding().SeedAsync().Wait();
            app.UseStaticFiles();

            app.Run();
        }
    }
}