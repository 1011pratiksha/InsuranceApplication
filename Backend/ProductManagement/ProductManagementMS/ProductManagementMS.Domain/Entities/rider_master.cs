using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Domain.Entities
{
    public class rider_master
    {
        [Key]
        public int rider_id { get; set; }

        public int product_id { get; set; }
        public string rider_name { get; set; } = string.Empty;
        public string? rider_description { get; set; }
        public decimal rider_premium { get; set; }
        public string status { get; set; } = "Active";
    }
}