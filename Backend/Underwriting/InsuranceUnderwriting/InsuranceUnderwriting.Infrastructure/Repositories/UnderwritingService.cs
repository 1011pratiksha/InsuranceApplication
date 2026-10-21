using InsuranceUnderwriting.Application.DTO;
using InsuranceUnderwriting.Application.Interfaces;
using InsuranceUnderwriting.Domain.Entities;
using InsuranceUnderwriting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Infrastructure.Repositories
{
    public class UnderwritingService : IUnderwritingService
    {

        AppDbContext data;
        public UnderwritingService(AppDbContext data)
        {
            this.data = data;
        }

        public async Task CreateUnderwritingCase(CreateUnderwritingCaseDto dto)
        {

            var d = new underwriting_case
            {
                quote_id = dto.quote_id,
                customer_id = dto.customer_id,
                product_id = dto.product_id,
                underwriting_date = DateTime.Now,
                status = "Pending"
            };
            data.underwriting_case.Add(d);

            await data.SaveChangesAsync();
        }

        public async Task<List<UnderwritingCaseResponseDto>> GetAllUnderwritingCase()
        {

           var d = await data.underwriting_case.ToListAsync();
           var result = d.Select(
                x => new UnderwritingCaseResponseDto
                {
                    underwriting_id = x.underwriting_id,
                    quote_id=x.quote_id,
                    customer_id=x.customer_id,
                    product_id=x.product_id,
                    underwriting_date=x.underwriting_date,
                    status=x.status
                }).ToList();

            return result;

         }

        public async Task<UnderwritingCaseResponseDto> GetById(long id)
        {
            var find = await data.underwriting_case.FindAsync(id);

            if(find == null)
            {
                return null;
            }

            var d = new UnderwritingCaseResponseDto
            {
                underwriting_id=find.underwriting_id,
                quote_id = find.quote_id,
                customer_id = find.customer_id,
                product_id = find.product_id,
                underwriting_date = find.underwriting_date,
                status = find.status
            };

            return d;   
        }



        public async Task update(UpdateUnderwritingCaseDto dto,long id)
        {
            var find =await data.underwriting_case.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if(find == null)
            {
                return;
            }

           find. quote_id = dto.quote_id;
           find.customer_id = dto.customer_id;
           find. product_id = dto.product_id;


           await data.SaveChangesAsync();
        }



        public async Task SaveMedicalDetails(SaveMedicalDetailsDto dto, long id)
        {
            var find = await data.underwriting_case.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if( find == null )
            {
                return;
            }

            var d = new medical_underwriting
            {
                underwriting_id = id,
                height = dto.height,
                weight = dto.weight,
                bmi = dto.weight/ (dto.height * dto.height),
                smoking = dto.smoking,
                alcohol = dto.alcohol,
                medical_history = dto.medical_history,
                medical_report_path = dto.medical_report_path,
            };

            data.medical_underwriting.Add(d);
            await data.SaveChangesAsync();
        }


        public async Task<MedicalDetailsResponseDto> GetMedicalDetails(long id)
        {
            var find = await data.medical_underwriting.FirstOrDefaultAsync(x =>x.underwriting_id == id);

            if(find == null)
            {
                return null;
            }


            var d = new MedicalDetailsResponseDto
            {
                medical_id = find.medical_id,
                underwriting_id = find.underwriting_id,
                height = find.height,
                weight = find.weight,
                bmi = find.bmi,
                smoking = find.smoking,
                alcohol = find.alcohol,
                medical_history = find.medical_history,
                medical_report_path = find.medical_report_path
            };
            return d;
        }

        public async Task UpdateMedicalDetails(UpdateMedicalDetailsDto dto, long id)
        {
            var find = await data.medical_underwriting.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if(find == null)
            {
                return;
            }

            find.height = dto.height;
            find.weight = dto.weight;
            find.bmi = dto.weight / (dto.height * dto.height);
            find.smoking = dto.smoking;
            find.alcohol = dto.alcohol;
            find.medical_history = dto.medical_history;
            find.medical_report_path = dto.medical_report_path;

            await data.SaveChangesAsync();
        }

        public async Task SaveFinanceDetails(SaveFinancialDetailsDto dto, long id)
        {
            var d = await data.underwriting_case.FirstOrDefaultAsync(x => x.underwriting_id == id);


            if(d == null)
            {
                return;
            }

            var a = new financial_underwriting
            {
                underwriting_id = id,
                annual_income = dto.annual_income,
                employer_name = dto.employer_name,
                occupation = dto.occupation,
                existing_insurance = dto.existing_insurance,
                loan_liability = dto.loan_liability
            };

            data.financial_underwriting.Add(a);
            await data.SaveChangesAsync();

        }

        public async Task<FinancialDetailsResponseDto> GetFinancialDetails(long id)
        {
            var d = await data.financial_underwriting.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if( d == null)
            {
                return null;
            }

            var s = new FinancialDetailsResponseDto
            {
                financial_id = d.financial_id,
                underwriting_id = d.underwriting_id,
                annual_income = d.annual_income,
                employer_name = d.employer_name,
                occupation = d.occupation,
                existing_insurance = d.existing_insurance,
                loan_liability = d.loan_liability
            };

            return s;

        }

        public async Task UpdateFinancialDetails(UpdateFinancialDetailsDto dto, long id)
        {
            var d = await data.financial_underwriting.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if (d == null)
            {
                return;
            }

            d.annual_income = dto.annual_income;
            d.employer_name = dto.employer_name;
            d.occupation = dto.occupation;
            d.existing_insurance = dto.existing_insurance;
            d.loan_liability = dto.loan_liability;

            await data.SaveChangesAsync();

        }

        public async Task ApproveUnderwriting(long id)
        {
            var find = await data.underwriting_case.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if(find == null)
            {
                return;
            }

            find.status = "Approved";

            await data.SaveChangesAsync();
        }

        public async Task RejectUnderwriting(long id)
        {
            var d =await data.underwriting_case.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if(d == null)
            {
                return;
            }

            d.status = "Rejected";

            await data.SaveChangesAsync();
        }



        public async Task ApplyPremiumLoading(ApplyPremiumLoadingDto dto, long id)
        {
            var d = await data.underwriting_case.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if( d == null)
            {
                throw new Exception("Underwriting Case Not Found");
            }


            var employeeExists = await data.Employees.AnyAsync(x => x.employee_id == dto.approved_by);

            if (!employeeExists)
            {
                throw new Exception("Employee Not Found");
            }


            var decision = new underwriting_decision
            {
                underwriting_id = id,
                decision = "Approved with Loading",
                loading_percentage = dto.loading_percentage,
                approved_by = dto.approved_by,
                decision_date = DateTime.Now,

            };

            data.underwriting_decision.Add(decision);

            await data.SaveChangesAsync();

        }

        public async Task<UnderwritingDecisionResponseDto> GetDecision(long id)
        {
            var d = await data.underwriting_decision.FirstOrDefaultAsync(x => x.underwriting_id == id);

            if(d == null)
            {
                return null;
            }

            var res = new UnderwritingDecisionResponseDto
            {
                decision_id = d.decision_id,
                underwriting_id = d.underwriting_id,
                decision = d.decision,
                loading_percentage = d.loading_percentage,
                exclusion_details = d.exclusion_details,
                rejection_reason = d.rejection_reason,
                approved_by = d.approved_by,
                decision_date= d.decision_date

            };

            return res;
        }
    }
}
