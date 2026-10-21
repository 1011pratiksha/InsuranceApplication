using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Domain.Entities
{
    public class Employee
    {
        [Key]
        public long employee_id { get; set; }

        public string employee_name { get; set; } 

        public string designation { get; set; } 
    }
}
