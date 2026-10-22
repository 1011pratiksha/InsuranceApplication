using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAgent.Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        public string? Description { get; set; }

        public decimal PremiumAmount { get; set; }

        public decimal SumInsured { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}