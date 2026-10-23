
using System;

namespace InsuranceAgent.Application.DTO
{
    public class CreateCommissionRuleDto
    {
        public int ProductId { get; set; }

        public decimal CommissionPercentage { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }
    }
}
