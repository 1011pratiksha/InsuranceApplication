
using System;

namespace UnderwritingMS.Application.DTO
{
    public class underwriting_audit_logDTO
    {
        public int audit_id { get; set; }

        public int underwriting_id { get; set; }

        public string action { get; set; } = string.Empty;

        public int? performed_by { get; set; }

        public string? remarks { get; set; }

        public DateTime action_date { get; set; }
    }
}
