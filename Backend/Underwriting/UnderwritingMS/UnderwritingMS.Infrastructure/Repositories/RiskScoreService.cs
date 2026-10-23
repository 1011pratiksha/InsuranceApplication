
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Application.Interface;
using UnderwritingMS.Domain.Entities;
using UnderwritingMS.Infrastructure.Data;

namespace UnderwritingMS.Infrastructure.Services
{
    public class RiskScoreService : IRiskScoreService
    {
        AppDbContext db;
        IMapper mapper;

        public RiskScoreService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CalculateRiskScore(
            int id, calculateRiskScoreDTO dto)
        {
            var underwriting = await db.underwriting_case.FindAsync(id);

            if (underwriting == null)
                throw new Exception("Underwriting case not found.");

            if (dto.age_score < 0 || dto.age_score > 20)
                throw new Exception("Age score must be between 0 and 20.");

            if (dto.medical_score < 0 || dto.medical_score > 30)
                throw new Exception("Medical score must be between 0 and 30.");

            if (dto.occupation_score < 0 || dto.occupation_score > 20)
                throw new Exception("Occupation score must be between 0 and 20.");

            if (dto.lifestyle_score < 0 || dto.lifestyle_score > 20)
                throw new Exception("Lifestyle score must be between 0 and 20.");

            if (dto.financial_score < 0 || dto.financial_score > 10)
                throw new Exception("Financial score must be between 0 and 10.");

            int total = dto.age_score
                      + dto.medical_score
                      + dto.occupation_score
                      + dto.lifestyle_score
                      + dto.financial_score;

            string category;

            if (total <= 30)
                category = "Low";
            else if (total <= 60)
                category = "Medium";
            else if (total <= 80)
                category = "High";
            else
                category = "Very High";

            var d = await db.risk_score
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
            {
                d = new risk_score
                {
                    underwriting_id = id
                };

                await db.risk_score.AddAsync(d);
            }

            d.age_score = dto.age_score;
            d.medical_score = dto.medical_score;
            d.occupation_score = dto.occupation_score;
            d.lifestyle_score = dto.lifestyle_score;
            d.financial_score = dto.financial_score;
            d.total_score = total;
            d.risk_category = category;

            await db.underwriting_audit_log.AddAsync(
                new underwriting_audit_log
                {
                    underwriting_id = id,
                    action = "Risk Score Calculated",
                    remarks = $"Total score: {total}; Risk category: {category}.",
                    action_date = DateTime.Now
                });

            await db.SaveChangesAsync();
        }

        public async Task<risk_scoreDTO?> GetRiskScore(int id)
        {
            var d = await db.risk_score
                .FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
                return null;

            return mapper.Map<risk_scoreDTO>(d);
        }
    }
}
