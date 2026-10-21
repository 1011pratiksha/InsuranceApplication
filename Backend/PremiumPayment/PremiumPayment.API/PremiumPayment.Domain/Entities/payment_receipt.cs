using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class payment_receipt
    {
        public long receipt_id { get; set; }                       // PK
        public long payment_id { get; set; }                       // FK(Premium_Payment)
        public string receipt_number { get; set; } = string.Empty; // VARCHAR(50) UNIQUE
        public DateTime receipt_date { get; set; }
        public string receipt_path { get; set; } = string.Empty;   // VARCHAR(500)

        public premium_payment premium_payment { get; set; } = null!;
    }

}
