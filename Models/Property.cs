using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PropertyInventorySystem.Models
{
    public class Property
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }
        public string Address { get; set; }

        [Range(1, 1000000, ErrorMessage = "Price must be between 1 and 1,000,000.")]
        public decimal Price { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public ICollection<SoldProperty> SoldProperties { get; set; }
    }
}
