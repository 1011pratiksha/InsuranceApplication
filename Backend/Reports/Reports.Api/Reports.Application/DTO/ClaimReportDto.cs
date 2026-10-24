using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class ClaimReportDto
    {
        public long ClaimId { get; set; }
        public long PolicyId { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal? SettledAmount { get; set; }
        public string? Status { get; set; }
        public DateTime? RegisteredDate { get; set; }
    }
}
