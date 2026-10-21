using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.DTO
{
    public class ServiceRequestResponseDto
    {
        public long request_id { get; set; }
        public string request_number { get; set; } 
        public long policy_id { get; set; }
        public long customer_id { get; set; }
        public string request_type { get; set; } 
        public DateTime request_date { get; set; }
        public string status { get; set; } 
    }
}
