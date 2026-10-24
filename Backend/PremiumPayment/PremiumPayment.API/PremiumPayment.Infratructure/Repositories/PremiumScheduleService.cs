
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PremiumPayment.Application.DTO;
using PremiumPayment.Application.Interfaces;
using PremiumPayment.Domain.Entities;
using PremiumPayment.Infrastructure.Data;
using PremiumPayment.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace PremiumPayment.Infrastructure.Repositories
{
    public class PremiumScheduleService : IPremiumScheduleService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IHttpClientFactory httpClientFactory;

        public PremiumScheduleService(
            ApplicationDbContext db,
            IMapper mapper,
            IHttpClientFactory httpClientFactory)
        {
            this.db = db;
            this.mapper = mapper;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<List<PremiumScheduleResponseDto>> GeneratePremiumSchedule(
            PremiumScheduleRequestDto request)
        {
            Console.WriteLine("1. GeneratePremiumSchedule started.");

            // Inter-service communication with Policy Issuance
            var client = httpClientFactory.CreateClient("PolicyIssuance");
            client.Timeout = TimeSpan.FromSeconds(15);

            Console.WriteLine("2. Calling Policy Issuance API.");

            using var response = await client.GetAsync(
                $"api/v1/policies/{request.PolicyId}");

            Console.WriteLine(
                $"3. Policy Issuance response received: {(int)response.StatusCode}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException(
                    "Policy not found in Policy Issuance service.");
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Policy Issuance service returned status code {(int)response.StatusCode}.");
            }

            Console.WriteLine("4. Checking existing premium schedule.");

            if (await db.premium_schedule.AnyAsync(
                s => s.policy_id == request.PolicyId))
            {
                throw new InvalidOperationException(
                    "Premium schedule already exists for this policy.");
            }

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

            decimal installmentAmount = Math.Round(
                request.AnnualPremium / periodsPerYear, 2);

            decimal lastInstallmentAmount =
                request.AnnualPremium * request.PolicyTermInYears
                - installmentAmount * (totalInstallments - 1);

            int[] reminderDays = { -15, -7, 0, 3, 7 };

            var schedules = new List<premium_schedule>();

            Console.WriteLine("5. Creating schedules and reminders.");

            for (int i = 1; i <= totalInstallments; i++)
            {
                var schedule = new premium_schedule
                {
                    policy_id = request.PolicyId,
                    premium_frequency = request.PremiumFrequency,
                    installment_no = i,
                    due_date = request.StartDate.Date.AddMonths(
                        monthsBetween * (i - 1)),
                    premium_amount = i == totalInstallments
                        ? lastInstallmentAmount
                        : installmentAmount,
                    late_fee = 0,
                    status = "Pending"
                };

                schedules.Add(schedule);
                db.premium_schedule.Add(schedule);

                foreach (int day in reminderDays)
                {
                    DateTime reminderDate = schedule.due_date.AddDays(day);

                    if (reminderDate >= DateTime.Today)
                    {
                        db.premium_reminder.Add(new PremiumReminders
                        {
                            premium_Schedule = schedule,
                            reminder_type = "Email",
                            reminder_date = reminderDate,
                            delivery_status = "Pending"
                        });
                    }
                }
            }

            Console.WriteLine("6. Saving schedules and reminders to database.");

            await db.SaveChangesAsync();

            Console.WriteLine("7. Database save completed.");

            return mapper.Map<List<PremiumScheduleResponseDto>>(schedules);
        }

        public async Task<List<PremiumScheduleResponseDto>> GetPremiumSchedule(
            int policyId)
        {
            var data = await db.premium_schedule
                .Where(s => s.policy_id == policyId)
                .OrderBy(s => s.installment_no)
                .ToListAsync();

            return mapper.Map<List<PremiumScheduleResponseDto>>(data);
        }

        public async Task<PremiumScheduleResponseDto> UpdateInstallment(
            int scheduleId,
            UpdateInstallmentDto request)
        {
            var schedule = await db.premium_schedule.FindAsync(scheduleId);

            if (schedule == null)
                return null;

            if (schedule.status == "Paid")
            {
                throw new InvalidOperationException(
                    "Paid installments cannot be modified.");
            }

            string[] allowedStatuses =
            {
                "Pending", "Paid", "Overdue", "Cancelled"
            };

            if (!allowedStatuses.Contains(request.Status))
            {
                throw new InvalidOperationException(
                    "Status must be Pending, Paid, Overdue or Cancelled.");
            }

            schedule.status = request.Status;

            if (request.DueDate.HasValue)
                schedule.due_date = request.DueDate.Value.Date;

            await db.SaveChangesAsync();

            return mapper.Map<PremiumScheduleResponseDto>(schedule);
        }

        public async Task UpdateOverdueInstallment()
        {
            decimal dailyPenaltyRate = 10;
            decimal maxLateFee = 1000;
            DateTime today = DateTime.Today;

            var installments = await db.premium_schedule
                .Where(s =>
                    (s.status == "Pending" || s.status == "Overdue")
                    && s.due_date < today)
                .ToListAsync();

            foreach (var item in installments)
            {
                int graceDays = item.premium_frequency == "Monthly" ? 15 : 30;
                DateTime graceEndDate = item.due_date.AddDays(graceDays);

                if (today <= graceEndDate)
                    continue;

                int overdueDays = (today - graceEndDate).Days;

                item.status = "Overdue";
                item.late_fee = Math.Min(
                    overdueDays * dailyPenaltyRate, maxLateFee);
            }

            await db.SaveChangesAsync();
        }

        public async Task SendTodaysReminder()
        {
            DateTime today = DateTime.Today;

            var reminders = await db.premium_reminder
                .Include(r => r.premium_Schedule)
                .Where(r =>
                    r.reminder_date >= today
                    && r.reminder_date < today.AddDays(1)
                    && r.delivery_status == "Pending"
                    && r.premium_Schedule.status != "Paid"
                    && r.premium_Schedule.status != "Cancelled")
                .ToListAsync();

            foreach (var reminder in reminders)
            {
                reminder.delivery_status = "Sent";
            }

            await db.SaveChangesAsync();
        }
    }
}
