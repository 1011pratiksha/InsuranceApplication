using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class ReportFilterDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string? Status { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }

        public long? ProductId { get; set; }
        public long? AgentId { get; set; }
        public long? BranchId { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
