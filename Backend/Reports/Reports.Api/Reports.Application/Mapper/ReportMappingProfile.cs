using AutoMapper;
using Reports.Application.DTO;
using Reports.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Mapper
{
   public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<ReportMaster, ReportMasterDto>();
            CreateMap<ReportSchedule, ReportScheduleDto>();
            CreateMap<ReportExecution, ReportExecutionDto>();

            CreateMap<CreateReportScheduleDto, ReportSchedule>();

            CreateMap<UpdateReportScheduleDto, ReportSchedule>()
                .ForMember(dest => dest.ScheduleId,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Report,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ReportId,
                    opt => opt.Ignore());
        }
    }
}
