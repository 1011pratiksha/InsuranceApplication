using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimManagement.Domain.Entities
{
    public class ClaimSettlement
    {
        [Key]
        public long settlement_id { get; set; }

        [ForeignKey("ClaimMaster")]
        public long claim_id { get; set; }

        public ClaimMaster? ClaimMaster { get; set; }
        public decimal approved_amount { get; set; }
        public string payment_mode { get; set; } = "";
        public string? transaction_reference { get; set; }
        public DateTime? settlement_date { get; set; }
        public string settlement_status { get; set; } = "Pending";
    }
}