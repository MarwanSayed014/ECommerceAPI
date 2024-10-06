using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class Role
    {
        [Key]
        [Column(Order = 1)]
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public static implicit operator Guid(Role v)
        {
            throw new NotImplementedException();
        }
    }
}
