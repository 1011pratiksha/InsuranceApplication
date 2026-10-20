using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Domain.Entities
{
    public class PremiumScheduler
    {
        [Key]
        public int scheduleId { get; set; }
        public int policyId { get; set; }
        public string premiumFrequency { get; set; }
        public int installmentNo { get; set; }
        public DateTime dueDate { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal premiumAmount { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal lateFee { get; set; } = 0;
        public string status { get; set; }

        public List<PremiumReminder> PremiumReminders { get; set; }
    }
}
