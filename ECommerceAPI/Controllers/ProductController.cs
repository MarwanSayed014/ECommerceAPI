using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.BL.Types;
using ECommerceAPI.BL.ViewModels;
using ECommerceAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductRepo _repo { get; }

        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repo.FindAllAsync();
        }


        [HttpPost]
        [Authorize(Roles = RoleTypes.Admin)]
        [Route("Create")]
        public async Task<bool> CreateAsync([FromForm] ProductViewModel model )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(model.file == null)
                        return false;
                    var dir =  Directory.GetCurrentDirectory() + "/wwwroot/Products/";
                    var ex = ServerFile.GetExtension(model.file.FileName) ;
                    var imageName = Guid.NewGuid() + ex;
                    var imagePath = dir + imageName;
                    var isUploaded = ServerFile.Upload(model.file, imagePath);
                    if (isUploaded)
                    {
                        Guid adminId = new Guid();
                        bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                        if (result == false)
                            return false;
                        Product Product = new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = model.Name,
                            CreatedDate = DateTime.Now,
                            AdminId = adminId,
                            BrandId = model.BrandId,
                            CategoryId = model.CategoryId,
                            ImageName = "https://" + Request.Host.Value + "/Products/" + imageName
                        };
                        return await _repo.CreateAsync(Product);
                    }
                    
                }
                return false;

            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleTypes.Admin)]
        [Route("Edit")]
        public async Task<bool> EditAsync([FromForm]ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid adminId = new Guid();
                    bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                    if (result == false)
                        return false;
                    Product oldProduct = (await _repo.FindAsync(x => x.Id == model.Id)).SingleOrDefault();
                    Product Product = new Product
                    {
                        Id = model.Id,
                        Name = model.Name,
                        CreatedDate = DateTime.Now,
                        AdminId = adminId,
                        BrandId = model.BrandId,
                        CategoryId = model.CategoryId
                    };
                    if (model.file == null)
                        Product.ImageName = oldProduct.ImageName;
                    else
                    {
                        var dir = Directory.GetCurrentDirectory() + "/wwwroot/Products/";
                        var oldImageName = oldProduct.ImageName.Split('/').Last();
                        ServerFile.Delete(dir + oldImageName);
                        var ex = ServerFile.GetExtension(model.file.FileName);
                        var imageName = Guid.NewGuid() + ex;
                        var imagePath = dir + imageName;
                        var isUploaded = ServerFile.Upload(model.file, imagePath);
                        if(isUploaded)
                            Product.ImageName = "https://" + Request.Host.Value + "/Products/" + imageName;
                    }
                    return await _repo.UpdateAsync(Product);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        [HttpPost]
        [Authorize(Roles = RoleTypes.Admin)]
        [Route("Delete")]
        public async Task<bool> DeleteAsync([FromBody] string id)
        {
            try
            {
                Guid ProductId = new Guid();
                bool result = Guid.TryParse(id, out ProductId);
                if (result == false)
                    return false;
                var Product = await _repo.FindWithKeysAsync(new Product { Id = ProductId, Name = "" });
                if (Product != null)
                {
                    var dir = Directory.GetCurrentDirectory() + "/wwwroot/Products/";
                    var imageName = Product.ImageName.Split('/').Last();
                    ServerFile.Delete(dir + imageName);
                    return await _repo.DeleteAsync(Product);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
