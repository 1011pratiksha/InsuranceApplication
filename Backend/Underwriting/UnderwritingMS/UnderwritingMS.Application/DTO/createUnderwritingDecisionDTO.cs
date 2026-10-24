
namespace UnderwritingMS.Application.DTO
{
    public class createUnderwritingDecisionDTO
    {
        public string decision { get; set; } = string.Empty;

        public decimal loading_percentage { get; set; }

        public string? exclusion_details { get; set; }

        public string? rejection_reason { get; set; }

        public int? approved_by { get; set; }
    }
}
