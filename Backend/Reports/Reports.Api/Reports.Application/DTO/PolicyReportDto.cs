using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class PolicyReportDto
    {
        public long PolicyId { get; set; }
        public long CustomerId { get; set; }
        public string? ProductName { get; set; }
        public string? Status { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal PremiumAmount { get; set; }
        public long? BranchId { get; set; }
    }
}
