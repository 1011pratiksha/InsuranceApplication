
namespace UnderwritingMS.Application.DTO
{
    public class medical_underwritingDTO
    {
        public int medical_id { get; set; }

        public int underwriting_id { get; set; }

        public decimal height { get; set; }

        public decimal weight { get; set; }

        public decimal bmi { get; set; }

        public string? blood_pressure { get; set; }

        public bool diabetes { get; set; }

        public bool heart_disease { get; set; }

        public bool cancer_history { get; set; }

        public bool smoking { get; set; }

        public bool alcohol { get; set; }

        public string? medical_history { get; set; }

        public string? medical_report_path { get; set; }
    }
}
