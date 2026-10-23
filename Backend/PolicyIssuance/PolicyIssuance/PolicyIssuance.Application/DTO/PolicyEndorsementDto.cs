using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyEndorsementDto
    {
        public int EndorsementId { get; set; }

        public int PolicyId { get; set; }

        public string EndorsementType { get; set; } = string.Empty;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime EndorsementDate { get; set; }
    }
}
