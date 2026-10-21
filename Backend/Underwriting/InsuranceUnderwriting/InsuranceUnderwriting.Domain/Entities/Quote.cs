using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class Quote
    {
        [Key]
        public long quote_id { get; set; }

        public string quote_number { get; set; } 

        [ForeignKey("Customer")]
        public long customer_id { get; set; }
        public Customer Customer { get; set; } 


        [ForeignKey("Insurance_Product")]
        public long product_id { get; set; }
        public Insurance_Product Insurance_Product { get; set; } 


        public decimal premium_amount { get; set; }
    }
}
