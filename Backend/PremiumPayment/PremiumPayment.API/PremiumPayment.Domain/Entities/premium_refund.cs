using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class premium_refund
    {
        public long refund_id { get; set; }                        // PK
        public long payment_id { get; set; }                       // FK(Premium_Payment)
        public decimal refund_amount { get; set; }                 // DECIMAL(15,2)
        public DateTime refund_date { get; set; }
        public string refund_status { get; set; } = string.Empty;  // see refund_status_values
        public long? approved_by { get; set; }                     // FK(Employee) -> other service

        public premium_payment premium_payment { get; set; } = null!;
    }

}
