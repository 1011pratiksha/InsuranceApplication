using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class FinancialReportDto
    {
        public string Period { get; set; } = string.Empty;
        public decimal PremiumIncome { get; set; }
        public decimal ClaimPayments { get; set; }
        public decimal OutstandingPremium { get; set; }
        public decimal Refunds { get; set; }
        public decimal Expenses { get; set; }
        public decimal? ProfitSummary { get; set; }
        public long? BranchId { get; set; }
    }
}
