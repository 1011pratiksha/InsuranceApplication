using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.DTO
{
    public class EndorsementDto
    {
        public long request_id { get; set; }
        public string endorsement_type { get; set; }
        public string? previous_value { get; set; }
        public string updated_value { get; set; }
    }
}
