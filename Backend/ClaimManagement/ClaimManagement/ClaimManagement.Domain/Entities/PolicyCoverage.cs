using System;
using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Domain.Entities
{
    public class PolicyCoverage
    {
        [Key]
        public long CoverageId { get; set; }

        public long PolicyId { get; set; }

        [MaxLength(150)]
        public string CoverageName { get; set; } = string.Empty;

        public decimal SumInsured { get; set; }

        public DateTime CoverageStartDate { get; set; }

        public DateTime CoverageEndDate { get; set; }
    }
}

