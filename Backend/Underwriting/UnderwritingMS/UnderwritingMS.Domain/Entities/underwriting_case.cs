
using System;
using System.ComponentModel.DataAnnotations;

namespace UnderwritingMS.Domain.Entities
{
    public class underwriting_case
    {
        [Key]
        public int underwriting_id { get; set; }

        public int quote_id { get; set; }

        public int customer_id { get; set; }

        public int product_id { get; set; }

        public DateTime underwriting_date { get; set; }

        public string status { get; set; } = "Pending";
    }
}
