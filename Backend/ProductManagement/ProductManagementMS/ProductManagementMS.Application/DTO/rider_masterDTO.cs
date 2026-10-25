using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductManagementMS.Application.DTO
{
    public class rider_masterDTO
    {
        public int rider_id { get; set; }

        public int product_id { get; set; }

        public string rider_name { get; set; } = string.Empty;

        public string? rider_description { get; set; }

        public decimal rider_premium { get; set; }

        public string status { get; set; } = "Active";
    }
}

