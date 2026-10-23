using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class NomineeDto
    {
        public int NomineeId { get; set; }
        public string NomineeName { get; set; }
        public string Relationship { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? MobileNumber { get; set; }
        public decimal SharePercentage { get; set; }
    }
}
