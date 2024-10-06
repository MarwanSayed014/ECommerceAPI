using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class OrderProducts : IEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Amount { get; set; }
    }
}
