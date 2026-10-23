using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class premium_payment
    {
        public long payment_id { get; set; }                       // PK
        public long schedule_id { get; set; }                      // FK(Premium_Schedule) -> other service
        public long policy_id { get; set; }                        // FK(Policy) -> other service
        public DateTime payment_date { get; set; }
        public decimal amount_paid { get; set; }                   // DECIMAL(15,2)
        public string payment_mode { get; set; } = string.Empty;   // see payment_mode_values
        public string transaction_reference { get; set; } = string.Empty; // VARCHAR(100)
        public string payment_status { get; set; } = string.Empty; // see payment_status_values

        public payment_receipt? payment_receipt { get; set; }
        public ICollection<premium_refund> premium_refunds { get; set; } = new List<premium_refund>();
    }

}
