using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Application.DTO
{
    public class CreateUnderwritingCaseDto
    {
        public long customer_id { get; set; }
        public long product_id { get; set; }
        public long quote_id { get; set; }
    }
}
