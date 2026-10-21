using InsuranceUnderwriting.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceUnderwriting.Application.Interfaces
{
    public interface IUnderwritingService
    {

        //Underwrting Details

        Task CreateUnderwritingCase(CreateUnderwritingCaseDto dto);

        Task<List<UnderwritingCaseResponseDto>> GetAllUnderwritingCase();

        Task<UnderwritingCaseResponseDto> GetById(long id);

        Task update(UpdateUnderwritingCaseDto dto, long id);


        //Now Medical Details

        Task SaveMedicalDetails(SaveMedicalDetailsDto dto, long id);
        Task<MedicalDetailsResponseDto> GetMedicalDetails(long id);
        Task UpdateMedicalDetails(UpdateMedicalDetailsDto dto, long id);




        //Now Finance Details

        Task SaveFinanceDetails(SaveFinancialDetailsDto dto, long id);
        Task<FinancialDetailsResponseDto> GetFinancialDetails(long id);
        Task UpdateFinancialDetails(UpdateFinancialDetailsDto dto, long id);


        //Now Approval Underwrting

        Task ApproveUnderwriting(long id);
        Task RejectUnderwriting(long id);


        //ApplyPremiumLoading(Decision_table)
        Task ApplyPremiumLoading(ApplyPremiumLoadingDto dto ,long id);

        Task<UnderwritingDecisionResponseDto> GetDecision(long id);
    }
}
