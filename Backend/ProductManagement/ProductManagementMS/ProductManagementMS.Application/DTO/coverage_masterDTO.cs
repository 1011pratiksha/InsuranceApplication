using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductManagementMS.Application.DTO
{
    public class coverage_masterDTO
    {
        public int coverage_id { get; set; }

        public int product_id { get; set; }

        public string coverage_name { get; set; } = string.Empty;

        public decimal? minimum_coverage { get; set; }

        public decimal? maximum_coverage { get; set; }

        public int? waiting_period_days { get; set; }

        public decimal? deductible_amount { get; set; }
    }
}

