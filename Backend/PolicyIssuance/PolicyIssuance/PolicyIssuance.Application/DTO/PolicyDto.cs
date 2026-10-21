using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyDto
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public int QuoteId { get; set; }

        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string PolicyStatus { get; set; } = string.Empty;
    }
}
