using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.DTO
{
    public class CreateServiceRequestDto
    {
        public long policy_id { get; set; }
        public long customer_id { get; set; }
        public string request_type { get; set; }

        public string? details { get; set; }
        public string? remarks { get; set; }

    }
}
