using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class PremiumSchedule
    {
        [Key]
        public long ScheduleId { get; set; }

        public long PolicyId { get; set; }

        public int InstallmentNo { get; set; }

        public DateTime DueDate { get; set; }

        public decimal PremiumAmount { get; set; }

        public decimal LateFee { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}

