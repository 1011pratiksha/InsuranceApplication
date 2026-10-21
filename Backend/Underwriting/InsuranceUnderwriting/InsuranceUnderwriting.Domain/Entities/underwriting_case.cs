using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class underwriting_case
    {
        [Key]
        public long underwriting_id { get; set; }

        [ForeignKey("Quote")]
        public long quote_id { get; set; }
        public Quote Quote { get; set; }


        [ForeignKey("Customer")]
        public long customer_id { get; set; }
        public Customer Customer { get; set; }


        [ForeignKey("Insurance_Product")]
        public long product_id { get; set; }
        public Insurance_Product Insurance_Product { get; set; }


        public DateTime underwriting_date { get; set; }

        public string status { get; set; } = "Pending";
    }
}
