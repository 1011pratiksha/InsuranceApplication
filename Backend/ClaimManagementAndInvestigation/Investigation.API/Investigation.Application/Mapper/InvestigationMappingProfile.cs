using AutoMapper;
using Investigation.Application.DTO;
using Investigation.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Investigation.Application.Mapper
{
    public class InvestigationMappingProfile : Profile
    {
        public InvestigationMappingProfile()
        {
            CreateMap<CreateInvestigationDto, InvestigationCase>();
        }
    }
}