using ECommerceAPI.BL.Interfaces;
using ECommerceAPI.BL.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class Order : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public decimal Total { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus status { get; set; }
    }
}
