using AutoMapper;
using PolicyIssuance.Application.DTO;
using PolicyIssuance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyIssuance.Application.Mapper
{
    public class PolicyIssuanceMapper : Profile
    {
        public PolicyIssuanceMapper()
        {
            CreateMap<Policy, PolicyDto>().ReverseMap();
            CreateMap<Policy, PolicyDetailDto>().ReverseMap();
            CreateMap<PolicyCoverage, PolicyCoverageDto>().ReverseMap();
            CreateMap<PolicyRider, PolicyRiderDto>().ReverseMap();
            CreateMap<PolicyDocument, PolicyDocumentDto>().ReverseMap();
            CreateMap<PolicyEndorsement, PolicyEndorsementDto>().ReverseMap();
            CreateMap<PolicyEndorsement, CreateEndorsementDto>().ReverseMap();
            CreateMap<PolicyHistory, PolicyHistoryDto>().ReverseMap();
        }
    }
}
