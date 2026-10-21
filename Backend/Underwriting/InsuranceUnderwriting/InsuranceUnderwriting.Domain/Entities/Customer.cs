using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class Customer
    {
        [Key]
        public long customer_id { get; set; }

        public string customer_name { get; set; } 

        public string? email { get; set; }

    }
}
