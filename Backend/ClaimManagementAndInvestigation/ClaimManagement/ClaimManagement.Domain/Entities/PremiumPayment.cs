using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class PremiumPayment
    {
        [Key]
        public long PaymentId { get; set; }

        public long ScheduleId { get; set; }

        public long PolicyId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal AmountPaid { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentMode { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? TransactionReference { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; } = string.Empty;
    }
}

