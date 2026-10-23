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
        [Key]
        public int schedule_id { get; set; }
        public int policy_id { get; set; }
        public string premium_frequency { get; set; }
        public int installment_no { get; set; }
        public DateTime due_date { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal premium_amount { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal late_fee { get; set; } = 0;
        public string status { get; set; }

        public List<premium_reminder> premium_Reminder { get; set; }
    }
}