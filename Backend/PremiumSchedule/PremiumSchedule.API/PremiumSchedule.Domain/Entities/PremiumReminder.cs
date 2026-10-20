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
        public int reminderId { get; set; }

        [ForeignKey("PremiumScheduler")]
        public int scheduleId { get; set; }
        public PremiumScheduler PremiumScheduler { get; set; }
        public string reminderType { get; set; }
        public DateTime reminderDate { get; set; }

    }
}
