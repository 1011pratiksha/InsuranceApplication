using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegistration.Application.DTO
{
    public class CreateNomineeDto
    {
        [Required]
        public string NomineeName { get; set; } = string.Empty;

        [Required]
        public string Relationship { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public string? MobileNumber { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Share percentage must be between 1 and 100.")]
        public decimal SharePercentage { get; set; }

        public string? GuardianName { get; set; }
    }

    public class NomineeDto : CreateNomineeDto
    {
        public int NomineeId { get; set; }
        public int CustomerId { get; set; }
    }
}
