
namespace UnderwritingMS.Application.DTO
{
    public class premiumLoadingDTO
    {
        public int underwriting_id { get; set; }

        public string risk_category { get; set; } = string.Empty;

        public decimal loading_percentage { get; set; }
    }
}
