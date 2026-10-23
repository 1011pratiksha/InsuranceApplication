
namespace UnderwritingMS.Application.DTO
{
    public class calculateRiskScoreDTO
    {
        public int age_score { get; set; }

        public int medical_score { get; set; }

        public int occupation_score { get; set; }

        public int lifestyle_score { get; set; }

        public int financial_score { get; set; }
    }
}
