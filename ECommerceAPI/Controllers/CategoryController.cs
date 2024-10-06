 using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.BL.Types;
using ECommerceAPI.BL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Claims;
using XAct;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryRepo _repo { get; }

        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repo.FindAllAsync();
        }


        [HttpPost]
        [Authorize(Roles = RoleTypes.Admin)]
        [Route("Create")]
        public async Task<bool> CreateAsync(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid adminId = new Guid();
                    bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value,out adminId);
                    if (result == false) 
                        return false;
                    Category category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        CreatedDate = DateTime.Now,
                        AdminId = adminId
                    };
                    return await _repo.CreateAsync(category);
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
        public async Task<bool> EditAsync(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid adminId = new Guid();
                    bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out adminId);
                    if (result == false)
                        return false;
                    Category category = new Category
                    {
                        Id = model.Id,
                        Name = model.Name,
                        CreatedDate = DateTime.Now,
                        AdminId = adminId
                    };
                    return await _repo.UpdateAsync(category);
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
        public async Task<bool> DeleteAsync([FromBody]string id)
        {
            try
            {
                Guid categoryId = new Guid();
                bool result = Guid.TryParse(id, out categoryId);
                if (result == false)
                    return false;
                var category = await _repo.FindWithKeysAsync(new Category { Id = categoryId, Name = "" });
                if (category != null)
                {
                    return await _repo.DeleteAsync(category);
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
