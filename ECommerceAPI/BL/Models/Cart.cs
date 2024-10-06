using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class Cart : IEntity
    {
        public Guid CartId { get; set; }
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
