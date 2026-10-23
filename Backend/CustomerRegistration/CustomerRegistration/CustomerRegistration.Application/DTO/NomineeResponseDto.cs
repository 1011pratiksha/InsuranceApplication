using System;

namespace CustomerRegistration.Application.DTO
{
    public class NomineeResponseDto
    {
        public int NomineeId { get; set; }
        public int CustomerId { get; set; }
        public string NomineeName { get; set; } = string.Empty;
        public string Relationship { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }
        public decimal SharePercentage { get; set; }
        public string? GuardianName { get; set; }
    }
}
