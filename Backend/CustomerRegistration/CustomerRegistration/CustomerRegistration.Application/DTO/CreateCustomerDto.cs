using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Application.DTO
{
    public class CreateCustomerDto
    {
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
    }
}
