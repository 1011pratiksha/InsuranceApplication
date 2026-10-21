using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyHistoryDto
    {
        public int HistoryId { get; set; }

        public int PolicyId { get; set; }

        public string Action { get; set; } = string.Empty;

        public int? PerformedBy { get; set; }

        public DateTime ActionDate { get; set; }

        public string? Remarks { get; set; }
    }
}
