using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRegistration.Application.DTO
{
    public class VerifyTwoFactorDto
    {
        public string ChallengeId { get; set; }
        public string Code { get; set; }
    }
}
