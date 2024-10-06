using ECommerceAPI.BL.Classes;
using ECommerceAPI.BL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.BL.Models
{
    public class User : Human, IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public required string Password { get; set; }
        public string? ProfileImgPath { get; set; }

    }
}
