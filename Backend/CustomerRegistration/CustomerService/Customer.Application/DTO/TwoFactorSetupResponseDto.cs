using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class TwoFactorSetupResponseDto
    {
        public string ChallengeId { get; set; }
        public string ManualEntryKey { get; set; }
        public string AuthenticatorUri { get; set; }
    }
}
