using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Domain.Entities
{
    public class premium_reminder
    {
        [Key]
        public int reminder_id { get; set; }

        [ForeignKey("PremiumScheduler")]
        public int schedule_id { get; set; }
        public premium_schedule premium_Schedule { get; set; }
        public string reminder_type { get; set; }
        public DateTime reminder_date { get; set; }
        public string delivery_status { get; set; }
    }
}
