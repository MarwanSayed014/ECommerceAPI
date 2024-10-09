using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class ShipmentDetails : IEntity
    {
        [Key]
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public int FloorNumber { get; set; }
        [Required]
        public string AddressInDetails { get; set; }
    }
}
