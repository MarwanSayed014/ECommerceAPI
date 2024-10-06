using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.BL.Types;
using ECommerceAPI.BL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public IBrandRepo _repo { get; }

        public BrandController(IBrandRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _repo.FindAllAsync();
        }


        [HttpPost]
        [Authorize(Roles = RoleTypes.Admin)]
        [Route("Create")]
        public async Task<bool> CreateAsync(BrandViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid adminId = new Guid();
                    bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                    if (result == false)
                        return false;
                    Brand Brand = new Brand
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        CreatedDate = DateTime.Now,
                        AdminId = adminId
                    };
                    return await _repo.CreateAsync(Brand);
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
        public async Task<bool> EditAsync(BrandViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid adminId = new Guid();
                    bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                    if (result == false)
                        return false;
                    Brand Brand = new Brand
                    {
                        Id = model.Id,
                        Name = model.Name,
                        CreatedDate = DateTime.Now,
                        AdminId = adminId
                    };
                    return await _repo.UpdateAsync(Brand);
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
                Guid BrandId = new Guid();
                bool result = Guid.TryParse(id, out BrandId);
                if (result == false)
                    return false;
                var Brand = await _repo.FindWithKeysAsync(new Brand { Id = BrandId, Name = "" });
                if (Brand != null)
                {
                    return await _repo.DeleteAsync(Brand);
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
