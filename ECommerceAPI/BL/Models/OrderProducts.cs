using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class OrderProducts : IEntity
    {
        [Key]
        public Guid OrderId { get; set; }
        [Key]
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
    }
}
