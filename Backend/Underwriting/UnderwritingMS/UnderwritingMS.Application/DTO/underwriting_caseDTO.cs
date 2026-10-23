
using System;

namespace UnderwritingMS.Application.DTO
{
    public class underwriting_caseDTO
    {
        public int underwriting_id { get; set; }

        public int quote_id { get; set; }

        public int customer_id { get; set; }

        public int product_id { get; set; }

        public DateTime underwriting_date { get; set; }

        public string status { get; set; } = string.Empty;
    }
}
