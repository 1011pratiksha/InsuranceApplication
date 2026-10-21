using System;
using System.Collections.Generic;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyDetailDto
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

        public List<PolicyCoverageDto> PolicyCoverages { get; set; } = new();

        public List<PolicyRiderDto> PolicyRiders { get; set; } = new();

        public List<PolicyDocumentDto> PolicyDocuments { get; set; } = new();

        public List<PolicyEndorsementDto> PolicyEndorsements { get; set; } = new();

        public List<PolicyHistoryDto> PolicyHistories { get; set; } = new();
    }
}
