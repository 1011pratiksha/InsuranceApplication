using PremiumPayment.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PremiumPayment.Application.Interfaces
{
    public interface IPremiumScheduleService
    {
        Task<List<PremiumScheduleResponseDto>> GeneratePremiumSchedule(PremiumScheduleRequestDto request);
        Task<List<PremiumScheduleResponseDto>> GetPremiumSchedule(int policy_id);
        Task<PremiumScheduleResponseDto> UpdateInstallment(int schedule_id, UpdateInstallmentDto request);
        Task UpdateOverdueInstallment();
        Task SendTodaysReminder();
    }
}
