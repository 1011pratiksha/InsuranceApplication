using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class PremiumReportDto
    {
        public long PaymentId { get; set; }
        public long PolicyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? Status { get; set; }
        public string? PaymentFrequency { get; set; }
    }
}
