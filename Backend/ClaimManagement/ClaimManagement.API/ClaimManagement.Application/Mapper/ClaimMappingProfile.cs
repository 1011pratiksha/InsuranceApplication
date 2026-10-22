using AutoMapper;
using ClaimManagement.Application.DTO;
using ClaimManagement.Domain.Entities;

namespace ClaimManagement.Application.Mapper
{
    public class ClaimMappingProfile : Profile
    {
        public ClaimMappingProfile()
        {
            // CreateClaimDto → ClaimMaster
            CreateMap<CreateClaimDto, ClaimMaster>()
                .ForMember(dest => dest.policy_id, opt => opt.MapFrom(src => src.PolicyId))
                .ForMember(dest => dest.customer_id, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.claim_type, opt => opt.MapFrom(src => src.ClaimType))
                .ForMember(dest => dest.incident_date, opt => opt.MapFrom(src => src.IncidentDate))
                .ForMember(dest => dest.estimated_loss, opt => opt.MapFrom(src => src.EstimatedLoss));

            // UpdateClaimDto → ClaimMaster
            CreateMap<UpdateClaimDto, ClaimMaster>()
                .ForMember(dest => dest.claim_type, opt => opt.MapFrom(src => src.ClaimType))
                .ForMember(dest => dest.incident_date, opt => opt.MapFrom(src => src.IncidentDate))
                .ForMember(dest => dest.estimated_loss, opt => opt.MapFrom(src => src.EstimatedLoss));

            // ClaimMaster → ClaimResponseDto
            CreateMap<ClaimMaster, ClaimResponseDto>()
                .ForMember(dest => dest.ClaimId, opt => opt.MapFrom(src => src.claim_id))
                .ForMember(dest => dest.ClaimNumber, opt => opt.MapFrom(src => src.claim_number))
                .ForMember(dest => dest.PolicyId, opt => opt.MapFrom(src => src.policy_id))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.customer_id))
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.claim_type))
                .ForMember(dest => dest.IncidentDate, opt => opt.MapFrom(src => src.incident_date))
                .ForMember(dest => dest.ClaimDate, opt => opt.MapFrom(src => src.claim_date))
                .ForMember(dest => dest.EstimatedLoss, opt => opt.MapFrom(src => src.estimated_loss))
                .ForMember(dest => dest.ClaimStatus, opt => opt.MapFrom(src => src.claim_status));

            // CreateClaimDocumentDto → ClaimDocument
            CreateMap<CreateClaimDocumentDto, ClaimDocument>()
                .ForMember(dest => dest.document_name, opt => opt.MapFrom(src => src.DocumentName))
                .ForMember(dest => dest.file_path, opt => opt.MapFrom(src => src.FilePath));

            // ClaimDocument → ClaimDocumentResponseDto
            CreateMap<ClaimDocument, ClaimDocumentResponseDto>()
                .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.document_id))
                .ForMember(dest => dest.ClaimId, opt => opt.MapFrom(src => src.claim_id))
                .ForMember(dest => dest.DocumentName, opt => opt.MapFrom(src => src.document_name))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => src.file_path))
                .ForMember(dest => dest.VerificationStatus, opt => opt.MapFrom(src => src.verification_status))
                .ForMember(dest => dest.UploadedDate, opt => opt.MapFrom(src => src.UploadedDate));

            // CreateSurveyDto → SurveyAssignment
            CreateMap<CreateSurveyDto, SurveyAssignment>()
                .ForMember(dest => dest.surveyor_id, opt => opt.MapFrom(src => src.SurveyorId))
                .ForMember(dest => dest.inspection_date, opt => opt.MapFrom(src => src.InspectionDate));

            // SurveyReportDto → SurveyAssignment
            CreateMap<SurveyReportDto, SurveyAssignment>()
                .ForMember(dest => dest.inspection_date, opt => opt.MapFrom(src => src.InspectionDate))
                .ForMember(dest => dest.report_path, opt => opt.MapFrom(src => src.ReportPath))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks));

            // SurveyAssignment → SurveyResponseDto
            CreateMap<SurveyAssignment, SurveyResponseDto>()
                .ForMember(dest => dest.SurveyId, opt => opt.MapFrom(src => src.survey_id))
                .ForMember(dest => dest.ClaimId, opt => opt.MapFrom(src => src.claim_id))
                .ForMember(dest => dest.SurveyorId, opt => opt.MapFrom(src => src.surveyor_id))
                .ForMember(dest => dest.AssignedDate, opt => opt.MapFrom(src => src.assigned_date))
                .ForMember(dest => dest.InspectionDate, opt => opt.MapFrom(src => src.inspection_date))
                .ForMember(dest => dest.ReportPath, opt => opt.MapFrom(src => src.report_path))
                .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks))
                .ForMember(dest => dest.SurveyStatus, opt => opt.MapFrom(src => src.survey_status));
        }
    }
}