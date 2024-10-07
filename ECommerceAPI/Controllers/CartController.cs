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
    [Authorize(Roles = RoleTypes.User)]
    [ApiController]
    public class CartController : ControllerBase
    {
        public ICartProductsRepo _cartProductsRepo { get; }
        public ICartRepo _cartRepo { get; }
        public IProductRepo _productRepo { get; }

        public CartController(ICartProductsRepo cartProductsRepo, ICartRepo cartRepo
            , IProductRepo productRepo)
        {
            _cartProductsRepo = cartProductsRepo;
            _cartRepo = cartRepo;
            _productRepo = productRepo;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CartViewModel>> GetAllAsync()
        {
            try
            {
                Guid userId = new Guid();
                bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                if (result == false)
                    return new List<CartViewModel>();
                var cartId = (await _cartRepo.FindAsync(x => x.UserId == userId)).Select(x => x.CartId).SingleOrDefault();
                var cartProducts = await _cartProductsRepo.FindAsync(x => x.CartId == cartId);
                var cartViewModelList = new List<CartViewModel>();
                foreach (var cartProduct in cartProducts)
                {
                    var product = (await _productRepo.FindAsync(x=>x.Id == cartProduct.ProductId)).SingleOrDefault();
                    if(product != null)
                    {
                        cartViewModelList.Add(new CartViewModel
                        {
                            ProductId = cartProduct.ProductId,
                            Amount = cartProduct.Amount,
                            ProductName = product.Name,
                            ImageName = product.ImageName
                        });
                    }
                }
                return cartViewModelList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("AddToCart")]
        public async Task<bool> AddToCartAsync([FromBody] string productId)
        {
            try
            {
                Guid _productId = new Guid();
                bool result1 = Guid.TryParse(productId, out _productId);
                Guid userId = new Guid();
                bool result2 = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                if (result1 == false || result2 == false)
                    return false;
                var cartId = (await _cartRepo.FindAsync(x=> x.UserId== userId)).Select(x=> x.CartId).SingleOrDefault();
                return await _cartProductsRepo.CreateAsync(new CartProducts
                {
                    ProductId = _productId,
                    CartId = cartId,
                    AddedDate= DateTime.Now,
                    Amount= 1,
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("RemoveFromCart")]
        public async Task<bool> RemoveFromCartAsync([FromBody] string productId)
        {
            try
            {
                Guid _productId = new Guid();
                bool result1 = Guid.TryParse(productId, out _productId);
                Guid userId = new Guid();
                bool result2 = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                if (result1 == false || result2 == false )
                    return false;
                var cartId = (await _cartRepo.FindAsync(x => x.UserId == userId)).Select(x => x.CartId).SingleOrDefault();
                var cartProduct = (await _cartProductsRepo.FindAsync(x=> x.CartId == cartId && x.ProductId == _productId)).SingleOrDefault();
                return await _cartProductsRepo.DeleteAsync(cartProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        [HttpPost]
        [Route("ChangeAmount")]
        public async Task<bool> ChangeAmountAsync(ProductAmountViewModel model)
        {
            try
            {
                if (model.Amount <= 0)
                    return false;
                Guid _productId = new Guid();
                bool result1 = Guid.TryParse(model.ProductId, out _productId);
                Guid userId = new Guid();
                bool result2 = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                if (result1 == false || result2 == false)
                    return false;
                var cartId = (await _cartRepo.FindAsync(x => x.UserId == userId)).Select(x => x.CartId).SingleOrDefault();
                var cartProduct = (await _cartProductsRepo.FindAsync(x => x.CartId == cartId && x.ProductId == _productId)).SingleOrDefault();
                cartProduct.Amount = model.Amount;
                return await _cartProductsRepo.UpdateAsync(cartProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("RemoveAllFromCart")]
        public async Task<bool> RemoveAllFromCartAsync()
        {
            try
            {
                Guid userId = new Guid();
                bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                if (result == false)
                    return false;
                var cartId = (await _cartRepo.FindAsync(x => x.UserId == userId)).Select(x => x.CartId).SingleOrDefault();
                var cartProducts = await _cartProductsRepo.FindAsync(x => x.CartId == cartId);
                foreach (var cartProduct in cartProducts)
                {
                    await _cartProductsRepo.DeleteAsync(cartProduct);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
