
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class PremiumLoadingService : IPremiumLoadingService
    {
        AppDbContext db;

        public PremiumLoadingService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<premiumLoadingDTO> CalculateLoading(int id)
        {
            var underwriting = await db.underwriting_case.FindAsync(id);

            if (underwriting == null)
                throw new Exception("Underwriting case not found.");

            var risk = await db.risk_score
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (risk == null)
                throw new Exception("Risk score not found. Calculate risk score first.");

            decimal loading;

            switch (risk.risk_category)
            {
                case "Low":
                    loading = 0;
                    break;

                case "Medium":
                    loading = 10;
                    break;

                case "High":
                    loading = 25;
                    break;

                case "Very High":
                    loading = 50;
                    break;

                default:
                    throw new Exception("Invalid risk category.");
            }

            return new premiumLoadingDTO
            {
                underwriting_id = id,
                risk_category = risk.risk_category,
                loading_percentage = loading
            };
        }
    }
}
