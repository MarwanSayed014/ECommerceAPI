using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class Category : IEntity
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
