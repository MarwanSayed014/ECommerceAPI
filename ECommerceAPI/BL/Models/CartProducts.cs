﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ECommerceAPI.BL.Interfaces;

namespace ECommerceAPI.BL.Models
{
    public class CartProducts : IEntity
    {
        [Key]
        public Guid CartId { get; set; }
        [Key]
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public DateTime AddedDate { get; set; }
        public int Amount { get; set; }
    }
}
