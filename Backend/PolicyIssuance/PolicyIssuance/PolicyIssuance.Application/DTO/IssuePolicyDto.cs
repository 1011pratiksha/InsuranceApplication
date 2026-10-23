using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class IssuePolicyDto
    {
        [Required]
        public int QuoteId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        public List<PolicyCoverageDto> Coverages { get; set; } = new();

        public List<PolicyRiderDto> Riders { get; set; } = new();
    }
}
