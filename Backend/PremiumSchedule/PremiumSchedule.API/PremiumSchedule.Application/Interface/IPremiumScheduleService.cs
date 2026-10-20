using PremiumSchedule.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumSchedule.Application.Interface
{
    public interface IPremiumScheduleService
    {
        Task<List<PremiumScheduleResponseDto>> GeneratePremiumSchedule(PremiumScheduleRequestDto request);
        Task<List<PremiumScheduleResponseDto>> GetPremiumSchedule(int policyId);
        Task<PremiumScheduleResponseDto> UpdateInstallment(int scheduleId, UpdateInstallmentDto request);
        Task UpdateOverdueInstallment();
        Task SendTodaysReminder();
    }
}
