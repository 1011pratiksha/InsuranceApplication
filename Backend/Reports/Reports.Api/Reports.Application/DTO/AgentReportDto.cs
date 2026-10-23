using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.DTO
{
    public class AgentReportDto
    {
        public long AgentId { get; set; }
        public string AgentName { get; set; } = string.Empty;
        public int PoliciesSold { get; set; }
        public decimal PremiumCollected { get; set; }
        public decimal Commission { get; set; }
        public int ActiveCustomers { get; set; }
        public decimal? ClaimRatio { get; set; }
    }
}
