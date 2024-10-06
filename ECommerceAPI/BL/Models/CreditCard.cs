using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceAPI.BL.Models
{
    public class CreditCard
    {
        [Key]
        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
