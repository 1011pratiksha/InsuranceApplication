using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reports.Application.DTO;
using Reports.Application.Interface;
using Reports.Domain.Entities;
using Reports.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Reports.Infrastructure.Data;

namespace Reports.Infrastructure.Services
{
    public class ReportScheduleService : IReportScheduleService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public ReportScheduleService(
            AppDbContext db,
            IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<ReportScheduleDto>> GetAllAsync()
        {
            var schedules = await db.ReportSchedules
                .AsNoTracking()
                .Include(x => x.Report)
                .ToListAsync();

            return mapper.Map<List<ReportScheduleDto>>(schedules);
        }

        public async Task<ReportScheduleDto?> GetByIdAsync(long scheduleId)
        {
            var schedule = await db.ReportSchedules
                .AsNoTracking()
                .Include(x => x.Report)
                .FirstOrDefaultAsync(x => x.ScheduleId == scheduleId);

            return schedule == null
                ? null
                : mapper.Map<ReportScheduleDto>(schedule);
        }

        public async Task<ReportScheduleDto?> CreateAsync(
            CreateReportScheduleDto dto)
        {
            var reportExists = await db.ReportMasters
                .AnyAsync(x => x.ReportId == dto.ReportId);

            if (!reportExists)
                return null;

            var schedule = mapper.Map<ReportSchedule>(dto);

            schedule.Status = "Active";

            db.ReportSchedules.Add(schedule);
            await db.SaveChangesAsync();

            return mapper.Map<ReportScheduleDto>(schedule);
        }

        public async Task<bool> DeleteAsync(long scheduleId)
        {
            var schedule = await db.ReportSchedules
                .FirstOrDefaultAsync(x => x.ScheduleId == scheduleId);

            if (schedule == null)
                return false;

            db.ReportSchedules.Remove(schedule);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<ReportScheduleDto?> UpdateAsync(
    long id,
    UpdateReportScheduleDto dto)
        {
            var schedule = await db.ReportSchedules
                .FirstOrDefaultAsync(x => x.ScheduleId == id);

            if (schedule == null)
                return null;

            schedule.ScheduleType = dto.ScheduleType;
            schedule.NextRunDate = dto.NextRunDate;
            schedule.EmailTo = dto.EmailTo;
            schedule.Status = dto.Status;

            await db.SaveChangesAsync();

            return mapper.Map<ReportScheduleDto>(schedule);
        }
    }
}

    

