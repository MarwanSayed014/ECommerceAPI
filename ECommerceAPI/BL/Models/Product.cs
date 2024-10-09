using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.BL.Models
{
    public class Product : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid AdminId { get; set; }
        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public Guid BrandId { get; set; }
        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImageName { get; set; }
        public decimal Price { get; set; }
    }
}
