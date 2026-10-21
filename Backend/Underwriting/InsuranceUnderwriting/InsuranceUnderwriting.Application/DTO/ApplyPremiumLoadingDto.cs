using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Application.DTO
{
    public class ApplyPremiumLoadingDto
    {
        public decimal loading_percentage { get; set; }
        public long approved_by { get; set; }
    }
}
