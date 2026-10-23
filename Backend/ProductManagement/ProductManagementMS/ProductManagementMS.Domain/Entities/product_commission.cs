using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Domain.Entities
{
    public class product_commission
    {
        [Key]
        public int commission_id { get; set; }

        public int product_id { get; set; }
        public decimal first_year_commission { get; set; }
        public decimal renewal_commission { get; set; }
        public decimal branch_commission { get; set; }
    }
}