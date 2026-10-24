
using System;

namespace InsuranceAgent.Application.DTO
{
    public class CommissionRuleResponseDto
    {
        public int CommissionRuleId { get; set; }

        public int ProductId { get; set; }

        public decimal CommissionPercentage { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
