using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerceAPI.BL.Interfaces;

namespace ECommerceAPI.BL.Models
{
    public class Brand : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid AdminId { get; set; }
        [ForeignKey("AdminId")]
        public virtual Admin Admin { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
