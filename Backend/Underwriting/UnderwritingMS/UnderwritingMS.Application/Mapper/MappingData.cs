
using AutoMapper;
using UnderwritingMS.Application.DTO;
using UnderwritingMS.Domain.Entities;

namespace UnderwritingMS.Application.Mapper
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<underwriting_case, underwriting_caseDTO>();
            CreateMap<createUnderwritingDTO, underwriting_case>();
            CreateMap<medical_underwriting, medical_underwritingDTO>();
            CreateMap<medical_underwritingDTO, medical_underwriting>();
            CreateMap<financial_underwriting, financial_underwritingDTO>();
            CreateMap<financial_underwritingDTO, financial_underwriting>();
            CreateMap<risk_score, risk_scoreDTO>();
            CreateMap<underwriting_decision, underwriting_decisionDTO>();
            CreateMap<createUnderwritingDecisionDTO, underwriting_decision>();
            CreateMap<underwriting_audit_log, underwriting_audit_logDTO>();
        }
    }
}
