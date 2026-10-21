using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class Insurance_Product
    {
        [Key]
        public long product_id { get; set; }

        public string product_name { get; set; }

        public string product_type { get; set; } 
    }
}
