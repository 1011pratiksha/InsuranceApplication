using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Application.DTO
{
    public class RegisterCustomerDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty; // Male, Female, Other

        [Required]
        public string MaritalStatus { get; set; } = string.Empty; // Single, Married, Divorced, Widowed

        [Required]
        public string Occupation { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Annual income must be a positive value.")]
        public decimal AnnualIncome { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string MobileNumber { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        public List<CreateAddressDto> Addresses { get; set; } = new();

        public List<CreateNomineeDto> Nominees { get; set; } = new();

        public CreateContactDto? Contact { get; set; }
    }
}
