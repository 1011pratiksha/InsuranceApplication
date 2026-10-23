using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Domain.Entities
{
    public class insurance_product
    {
        [Key]
        public int product_id { get; set; }

        public int category_id { get; set; }
        public string product_code { get; set; } = string.Empty;
        public string product_name { get; set; } = string.Empty;
        public string product_type { get; set; } = string.Empty;
        public string? description { get; set; }
        public int? minimum_age { get; set; }
        public int? maximum_age { get; set; }
        public decimal? minimum_sum_insured { get; set; }
        public decimal? maximum_sum_insured { get; set; }
        public string status { get; set; } = "Inactive";
    }
}
