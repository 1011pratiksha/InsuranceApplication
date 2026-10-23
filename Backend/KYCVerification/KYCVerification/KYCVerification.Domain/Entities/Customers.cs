using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KYCVerification.Domain.Entities
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string CustomerCode { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string MaritalStatus { get; set; } = string.Empty;

        [Required]
        public string Occupation { get; set; } = string.Empty;

        [Required]
        public decimal AnnualIncome { get; set; }

        [Required]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string CustomerStatus { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //public List<CustomerAddress> Addresses { get; set; } = new();

        //public List<CustomerContact> Contacts { get; set; } = new();

        //public List<Nominee> Nominees { get; set; } = new();

        //public List<CustomerDocument> Documents { get; set; } = new();
    }
}
