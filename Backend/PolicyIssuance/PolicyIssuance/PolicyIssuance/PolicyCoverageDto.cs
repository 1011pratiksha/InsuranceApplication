using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PolicyIssuance.Application.DTO
{
    public class PolicyCoverageDto
    {
        public int CoverageId { get; set; }

        public int PolicyId { get; set; }

        [Required]
        public string CoverageName { get; set; } = string.Empty;

        [Required]
        public decimal SumInsured { get; set; }

        [Required]
        public DateTime CoverageStartDate { get; set; }

        [Required]
        public DateTime CoverageEndDate { get; set; }
    }
}
