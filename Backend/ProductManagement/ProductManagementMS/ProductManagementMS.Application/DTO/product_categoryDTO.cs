using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Application.DTO
{
    public class product_categoryDTO
    {
        public int category_id { get; set; }
        public string category_name { get; set; } = string.Empty;
        public string? description { get; set; }
        public string status { get; set; } = "Active";
    }
}
