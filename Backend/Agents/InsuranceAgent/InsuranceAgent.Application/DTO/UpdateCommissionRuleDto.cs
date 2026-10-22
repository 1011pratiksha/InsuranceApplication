
using System;

namespace InsuranceAgent.Application.DTO
{
    public class UpdateCommissionRuleDto
    {
        public decimal CommissionPercentage { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }
    }
}
