using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Models;
using ECommerceAPI.BL.Types;
using ECommerceAPI.DAL.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [Route("ECommerceAPI/[controller]")]
    [Authorize(RoleTypes.User)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public ICartProductsRepo _cartProductsRepo { get; }
        public IOrderRepo _orderRepo { get; }
        public IOrderProductsRepo _orderProductsRepo { get; }
        public IProductRepo _productRepo { get; }
        public ICartRepo _cartRepo { get; }

        public OrderController(ICartRepo cartRepo, ICartProductsRepo cartProductsRepo,
            IOrderRepo orderRepo, IOrderProductsRepo orderProductsRepo,
            IProductRepo productRepo)
        {
            _cartRepo = cartRepo;
            _cartProductsRepo = cartProductsRepo;
            _orderRepo = orderRepo;
            _orderProductsRepo = orderProductsRepo;
            _productRepo = productRepo;
        }
        [HttpPost]
        [Route("CreateFromCart")]
        public async Task<Guid?> CreateFromCartAsync()
        {
            try
            {
                Guid userId = new Guid();
                bool result = Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out userId);
                if (result == false)
                    return null;
                var cartId = (await _cartRepo.FindAsync(x => x.UserId == userId)).Select(x => x.CartId).SingleOrDefault();
                var cartProducts = await _cartProductsRepo.FindAsync(x => x.CartId == cartId);
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    status = OrderStatus.Pending,
                    Total = 0
                };
                if (await _orderRepo.CreateAsync(order))
                {
                    foreach (var cartProduct in cartProducts)
                    {
                        await _orderProductsRepo.CreateAsync(new OrderProducts
                        {
                            Amount= cartProduct.Amount,
                            OrderId = order.Id,
                            ProductId = cartProduct.ProductId
                        });
                        await _cartProductsRepo.DeleteAsync(cartProduct);
                        var product = (await _productRepo.FindAsync(x => x.Id == cartProduct.ProductId)).SingleOrDefault();
                        order.Total += cartProduct.Amount * product.Price;
                    }
                    await _orderRepo.UpdateAsync(order);
                    return order.Id;
                }
                return null;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
