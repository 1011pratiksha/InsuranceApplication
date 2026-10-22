using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyServicing.Domain.Entities
{
    public class Quote
    {
        [Key]
        public long quote_id { get; set; }
        public string quote_number { get; set; } = string.Empty;
        public long customer_id { get; set; }
        public long product_id { get; set; }
        public int policy_term { get; set; }
        public decimal sum_insured { get; set; }
        public long y_id { get; set; }
        public DateTime quote_date { get; set; }
        public DateTime expiry_date { get; set; }
    }
}
