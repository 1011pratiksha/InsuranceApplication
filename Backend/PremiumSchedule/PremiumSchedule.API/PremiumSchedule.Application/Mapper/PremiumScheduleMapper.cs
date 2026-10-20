using AutoMapper;
using PremiumSchedule.Application.DTO;
using PremiumSchedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Application.Mapper
{
    public class PremiumScheduleMapper : Profile
    {
        public PremiumScheduleMapper()
        {
            CreateMap<PremiumScheduler, PremiumScheduleResponseDto>().ReverseMap();

            SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        }
    }
}
