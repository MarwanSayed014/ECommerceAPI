using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class Cart : IEntity
    {
        [Key]
        public Guid CartId { get; set; }
        [ForeignKey("UserId")]
        [Key]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
