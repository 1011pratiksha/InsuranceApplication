using AutoMapper;
using   PremiumPayment.Application.DTO;
using PremiumPayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Mapper
{
    public class PremiumScheduleMapper : Profile
    {
        public PremiumScheduleMapper()
        {
            CreateMap<premium_schedule, PremiumScheduleResponseDto>().ReverseMap();

            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        }
    }
}
