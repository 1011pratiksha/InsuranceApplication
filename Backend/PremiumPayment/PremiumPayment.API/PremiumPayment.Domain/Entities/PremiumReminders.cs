
using System;
using System.ComponentModel.DataAnnotations;
using PremiumPayment.Domain.Entities;

namespace PremiumPayment.Domain.Entities
{
    public class PremiumReminders
    {
        [Key]
        public int reminder_id { get; set; }

        public int schedule_id { get; set; }

        public premium_schedule premium_Schedule { get; set; }

        public string reminder_type { get; set; }

        public DateTime reminder_date { get; set; }

        public string delivery_status { get; set; }
    }
}
