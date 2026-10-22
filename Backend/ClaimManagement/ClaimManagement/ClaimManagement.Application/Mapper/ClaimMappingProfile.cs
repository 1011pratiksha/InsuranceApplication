using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Domain.Entities;

namespace ClaimManagement.Application.Mapper
{
    public class ClaimMappingProfile : Profile
    {
        public ClaimMappingProfile()
        {
            CreateMap<CreateClaimDto, Claim>();
            CreateMap<UpdateClaimDto, Claim>();
            CreateMap<Claim, ClaimResponseDto>();

            CreateMap<CreateClaimDocumentDto, ClaimDocument>();
            CreateMap<ClaimDocument, ClaimDocumentResponseDto>();

            CreateMap<CreateSurveyDto, SurveyAssignment>();
            CreateMap<SurveyReportDto, SurveyAssignment>();
            CreateMap<SurveyAssignment, SurveyResponseDto>();
        }
    }
}