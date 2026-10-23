using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class Customer
    {
        [Key]
        public long CustomerId { get; set; }

        [Required]
        [MaxLength(20)]
        public string CustomerCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string MaritalStatus { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Occupation { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; }

        [MaxLength(15)]
        public string? MobileNumber { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string CustomerStatus { get; set; } = "Active";

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
