using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimAssessment
    {
        [Key]
        public long assessment_id { get; set; }

        [ForeignKey("ClaimMaster")]
        public long claim_id { get; set; }

        public ClaimMaster? ClaimMaster { get; set; }
        public decimal assessed_amount { get; set; }
        public decimal deductible { get; set; }
        public decimal depreciation { get; set; }
        public decimal non_payable { get; set; }
        public decimal net_payable { get; set; }
    }
}