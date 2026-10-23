using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class LoginResponseDto
    {
        public bool RequiresTwoFactor { get; set; }
        public string? ChallengeId { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? ExpiresAtUtc { get; set; }
    }
}
