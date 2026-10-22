using System;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAgent.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? MobileNo { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}