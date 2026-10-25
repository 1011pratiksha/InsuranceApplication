using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementMS.Domain.Entities
{
    public class premium_frequency
    {
        [Key]
        public int frequency_id { get; set; }

        public string frequency_name { get; set; } = string.Empty;
        public int installment_count { get; set; }
        public string status { get; set; } = "Active";
    }
}