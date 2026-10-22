using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Domain.Entities
{
    public class CommissionRule
    {
        [Key]
        public int CommissionRuleId { get; set; }


        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
       

        public decimal CommissionPercentage { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
