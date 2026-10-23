using AutoMapper;
using Investigation.Application.DTO;
using Investigation.Domain.Entities;

namespace Investigation.Application.Mapper
{
    public class InvestigationMappingProfile : Profile
    {
        public InvestigationMappingProfile()
        {
            CreateMap<CreateInvestigationDto, InvestigationCase>();
            CreateMap<InvestigationReportDto, InvestigationCase>();
            CreateMap<InvestigationCase, InvestigationResponseDto>();
        }
    }
}