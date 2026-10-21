using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class premium_schedule
    {
        public long schedule_id { get; set; }
        public long policy_id { get; set; }
        public int installment_no { get; set; }
        public DateTime due_date { get; set; }
        public decimal premium_amount { get; set; }
        public decimal late_fee { get; set; } = 0;
        public string status { get; set; } = string.Empty;   // Pending, Paid, Partial Paid, Overdue, Cancelled
    }
}