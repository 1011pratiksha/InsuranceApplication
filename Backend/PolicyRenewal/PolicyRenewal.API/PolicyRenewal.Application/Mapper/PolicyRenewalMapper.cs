using AutoMapper;
using PolicyRenewal.Application.DTO;
using PolicyRenewal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicyRenewal.Application.Mapper
{
    public class PolicyRenewalMapper : Profile
    {
        public PolicyRenewalMapper()
        {
            CreateMap<Policy_Renewal, PolicyRenewalResponseDto>().ReverseMap();

            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        }
    }
}
