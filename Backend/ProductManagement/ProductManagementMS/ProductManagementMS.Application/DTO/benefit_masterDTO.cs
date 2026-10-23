using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Application.DTO
{
    public class benefit_masterDTO
    {
        public int benefit_id { get; set; }

        public int product_id { get; set; }

        public string benefit_name { get; set; } = string.Empty;

        public string? benefit_description { get; set; }

        public decimal? benefit_limit { get; set; }
    }
}
