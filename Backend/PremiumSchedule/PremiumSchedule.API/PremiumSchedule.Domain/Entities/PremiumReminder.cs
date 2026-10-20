using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Domain.Entities
{
    public class PremiumReminder
    {
        [Key]
        public int reminder_id { get; set; }

        [ForeignKey("PremiumScheduler")]
        public int schedule_id { get; set; }
        public PremiumScheduler PremiumScheduler { get; set; }
        public string reminder_type { get; set; }
        public DateTime reminder_date { get; set; }
        public string delivery_status { get; set; }
    }
}
