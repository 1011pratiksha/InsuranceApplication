using AutoMapper;
using ClaimManagement.Domain.Entities;
using ClaimManagement.Application.DTO;

namespace ClaimManagement.Application.Mapper
{
    public class InvestigationMappingProfile : Profile
    {
        public InvestigationMappingProfile()
        {
            // CreateInvestigationDto → Investigation
            CreateMap<CreateInvestigationDto, Investigation>()
                .ForMember(dest => dest.investigator_id, opt => opt.MapFrom(src => src.InvestigatorId))
                .ForMember(dest => dest.investigation_reason, opt => opt.MapFrom(src => src.InvestigationReason));

            // InvestigationReportDto → Investigation
            CreateMap<InvestigationReportDto, Investigation>()
                .ForMember(dest => dest.report_path, opt => opt.MapFrom(src => src.ReportPath));

            // Investigation → InvestigationResponseDto
            CreateMap<Investigation, InvestigationResponseDto>()
                .ForMember(dest => dest.InvestigationId, opt => opt.MapFrom(src => src.investigation_id))
                .ForMember(dest => dest.ClaimId, opt => opt.MapFrom(src => src.claim_id))
                .ForMember(dest => dest.InvestigatorId, opt => opt.MapFrom(src => src.investigator_id))
                .ForMember(dest => dest.InvestigationReason, opt => opt.MapFrom(src => src.investigation_reason))
                .ForMember(dest => dest.InvestigationStatus, opt => opt.MapFrom(src => src.investigation_status))
                .ForMember(dest => dest.ReportPath, opt => opt.MapFrom(src => src.report_path))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.AssignedDate))
                .ForMember(dest => dest.CompletedDate, opt => opt.MapFrom(src => src.CompletedDate));
        }
    }
}