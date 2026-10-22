using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InsuranceAgent.Domain.Entities
{
    public class Policy
    {
        [Key]
        public int PolicyId { get; set; }

        public int QuoteId { get; set; }

        [ForeignKey("QuoteId")]
        public Quote Quote { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string PolicyStatus { get; set; }
    }
}