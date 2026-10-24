using InsuranceAgent.Application.DTO;
using InsuranceAgent.Application.Interfaces;
using InsuranceAgent.Application.Mapper;
using InsuranceAgent.Domain.Entities;
using InsuranceAgent.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAgent.Infrastructure.Repositories
{
    public class CommissionRuleService : ICommissionRuleService
    {

        AppDbContext data;
        ProductClient productClient;

        public CommissionRuleService(AppDbContext data, ProductClient productClient)
        {
            this.data = data;
            this.productClient = productClient;
        }

       

        public async Task CreateCommissionRule(CreateCommissionRuleDto dto)
        {
            var product = await productClient.GetProductById(dto.ProductId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            var commissionRule = new CommissionRule
            {
                ProductId = dto.ProductId,
                CommissionPercentage = dto.CommissionPercentage,
                EffectiveFrom = dto.EffectiveFrom,
                EffectiveTo = dto.EffectiveTo,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.Now
            };

            await data.CommissionRules.AddAsync(commissionRule);

            await data.SaveChangesAsync();
        }



        public async Task<List<CommissionRuleResponseDto>> GetAllCommissionRules()
        {

            var commissionRules = await data.CommissionRules.Select(cr => new CommissionRuleResponseDto
              {
                CommissionRuleId = cr.CommissionRuleId,
                ProductId = cr.ProductId,
                CommissionPercentage = cr.CommissionPercentage,
                EffectiveFrom = cr.EffectiveFrom,
                EffectiveTo = cr.EffectiveTo,
                IsActive = cr.IsActive,
                CreatedAt = cr.CreatedAt
              }).ToListAsync();

                return commissionRules;
        }




        public async Task<CommissionRuleResponseDto> GetCommissionRuleByProductId(int productId)
        {
            var rule = await data.CommissionRules.FirstOrDefaultAsync(cr => cr.ProductId == productId && cr.IsActive);


            if (rule == null)
            {
                throw new Exception("Commission rule not found.");
            }


            return new CommissionRuleResponseDto
            {
                CommissionRuleId = rule.CommissionRuleId,
                ProductId = rule.ProductId,
                CommissionPercentage = rule.CommissionPercentage,
                EffectiveFrom = rule.EffectiveFrom,
                EffectiveTo = rule.EffectiveTo,
                IsActive = rule.IsActive,
                CreatedAt = rule.CreatedAt
            };
        }




        public async Task UpdateCommissionRule(int ruleId, UpdateCommissionRuleDto dto)
        {
            var rule = await data.CommissionRules .FirstOrDefaultAsync(cr => cr.CommissionRuleId == ruleId);

            if (rule == null)
            {
                throw new Exception("Commission rule not found.");
            }

            rule.CommissionPercentage = dto.CommissionPercentage;
            rule.EffectiveFrom = dto.EffectiveFrom;
            rule.EffectiveTo = dto.EffectiveTo;
            rule.IsActive = dto.IsActive;

            await data.SaveChangesAsync();

        }
    }
}
