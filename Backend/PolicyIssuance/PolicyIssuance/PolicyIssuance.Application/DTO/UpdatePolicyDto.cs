using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class UpdatePolicyDto
    {
        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string? PolicyStatus { get; set; }
    }
}
