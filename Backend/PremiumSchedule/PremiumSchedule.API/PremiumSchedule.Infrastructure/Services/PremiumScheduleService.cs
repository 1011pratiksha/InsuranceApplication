using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PremiumSchedule.Application.DTO;
using PremiumSchedule.Application.Interface;
using PremiumSchedule.Domain.Entities;
using PremiumSchedule.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumSchedule.Infrastructure.Services
{
    public class PremiumScheduleService : IPremiumScheduleService
    {
        AppDbContext db;
        IMapper mapper;
        public PremiumScheduleService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<List<PremiumScheduleResponseDto>> GeneratePremiumSchedule(PremiumScheduleRequestDto request)
        {
            if (await db.PremiumSchedulers.AnyAsync(s => s.policyId == request.PolicyId))
                throw new InvalidOperationException("Premium schedule already exists for this policy.");

            int monthsBetween = 12;
            if (request.PremiumFrequency == "Monthly")
            {
                monthsBetween = 1;
            }
            else if (request.PremiumFrequency == "Quarterly")
            {
                monthsBetween = 3;
            }
            else if (request.PremiumFrequency == "Half-Yearly")
            {
                monthsBetween = 6;
            }

            int periodsPerYear = 12 / monthsBetween;
            int totalInstallments = request.PolicyTermInYears * periodsPerYear;
            decimal installmentAmount = Math.Round(request.AnnualPremium / periodsPerYear, 2);
            decimal lastInstallmentAmount = request.AnnualPremium * request.PolicyTermInYears - installmentAmount * (totalInstallments - 1);
            int[] reminderDays = { -15, -7, 0, 3, 7 };

            var schedules = new List<PremiumScheduler>();

            for (int i = 1; i <= totalInstallments; i++)
            {
                var schedule = new PremiumScheduler
                {
                    policyId = request.PolicyId,
                    premiumFrequency = request.PremiumFrequency,
                    installmentNo = i,
                    dueDate = request.StartDate.Date.AddMonths(monthsBetween * (i - 1)),
                    premiumAmount = i == totalInstallments ? lastInstallmentAmount : installmentAmount,
                    lateFee = 0,
                    status = "Pending"
                };

                schedules.Add(schedule);
                db.PremiumSchedulers.Add(schedule);

                foreach (int day in reminderDays)
                {
                    DateTime reminderDate = schedule.dueDate.AddDays(day);

                    if (reminderDate >= DateTime.Today)
                    {
                        db.PremiumReminders.Add(new PremiumReminder
                        {
                            PremiumScheduler = schedule,
                            reminderType = "Email",
                            reminderDate = reminderDate
                        });
                    }
                }
            }

            await db.SaveChangesAsync();

            return mapper.Map<List<PremiumScheduleResponseDto>>(schedules);
        }

        public async Task<List<PremiumScheduleResponseDto>> GetPremiumSchedule(int policyId)
        {
            var data = await db.PremiumSchedulers
                .Where(s => s.policyId == policyId)
                .OrderBy(s => s.installmentNo)
                .ToListAsync();

            return mapper.Map<List<PremiumScheduleResponseDto>>(data);
        }

        public async Task<PremiumScheduleResponseDto> UpdateInstallment(int scheduleId, UpdateInstallmentDto request)
        {
            var schedule = await db.PremiumSchedulers.FindAsync(scheduleId);

            if (schedule == null)
                return null;

            if (schedule.status == "Paid")
                throw new InvalidOperationException("Paid installments cannot be modified.");

            string[] allowedStatuses = { "Pending", "Paid", "Overdue", "Cancelled" };

            if (!allowedStatuses.Contains(request.Status))
                throw new InvalidOperationException("Status must be Pending, Paid, Overdue or Cancelled.");

            schedule.status = request.Status;

            if (request.DueDate.HasValue)
                schedule.dueDate = request.DueDate.Value.Date;

            await db.SaveChangesAsync();

            return mapper.Map<PremiumScheduleResponseDto>(schedule);
        }

        public async Task UpdateOverdueInstallment()
        {
            decimal dailyPenaltyRate = 10;
            decimal maxLateFee = 1000;
            DateTime today = DateTime.Today;

            var installments = await db.PremiumSchedulers
                .Where(s => (s.status == "Pending" || s.status == "Overdue") && s.dueDate < today)
                .ToListAsync();

            foreach (var item in installments)
            {
                int graceDays = item.premiumFrequency == "Monthly" ? 15 : 30;
                DateTime graceEndDate = item.dueDate.AddDays(graceDays);

                if (today <= graceEndDate)
                    continue;

                int overdueDays = (today - graceEndDate).Days;

                item.status = "Overdue";
                item.lateFee = Math.Min(overdueDays * dailyPenaltyRate, maxLateFee);
            }

            await db.SaveChangesAsync();
        }

        public async Task SendTodaysReminder()
        {
            DateTime today = DateTime.Today;

            var reminders = await db.PremiumReminders
                .Include(r => r.PremiumScheduler)
                .Where(r => r.reminderDate >= today
                         && r.reminderDate < today.AddDays(1)
                         && r.PremiumScheduler.status != "Paid"
                         && r.PremiumScheduler.status != "Cancelled")
                .ToListAsync();
        }
    }
}