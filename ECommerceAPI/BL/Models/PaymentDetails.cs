using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerceAPI.BL.Types;

namespace ECommerceAPI.BL.Models
{
    public class PaymentDetails : IEntity
    {
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Required]
        public PaymentMethods PaymentMethod { get; set; }

    }
}
