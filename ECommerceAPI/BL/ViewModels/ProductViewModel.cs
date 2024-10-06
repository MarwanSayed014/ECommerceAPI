using ECommerceAPI.BL.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public IFormFile? file { get; set; }
    }
}
