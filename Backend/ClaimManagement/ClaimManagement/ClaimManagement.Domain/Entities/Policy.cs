using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class Policy
    {
        [Key]
        public long PolicyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        public long QuoteId { get; set; }

        public long CustomerId { get; set; }

        public long ProductId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string PolicyStatus { get; set; } = string.Empty;
    }
}
