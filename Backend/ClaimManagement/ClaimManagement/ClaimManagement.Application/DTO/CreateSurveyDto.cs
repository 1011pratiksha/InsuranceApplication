using System.ComponentModel.DataAnnotations;

namespace ClaimManagement.Application.DTO
{
    public class CreateSurveyDto
    {
        [Range(1, int.MaxValue)]
        public int SurveyorId { get; set; }

        public DateTime? InspectionDate { get; set; }
    }
}