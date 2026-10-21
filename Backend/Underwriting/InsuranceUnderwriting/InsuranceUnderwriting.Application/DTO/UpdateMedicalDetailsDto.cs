using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Application.DTO
{
    public class UpdateMedicalDetailsDto
    {
        public decimal height { get; set; }

        public decimal weight { get; set; }

        public bool smoking { get; set; }

        public bool alcohol { get; set; }

        public string? medical_history { get; set; }

        public string? medical_report_path { get; set; }

    }
}
