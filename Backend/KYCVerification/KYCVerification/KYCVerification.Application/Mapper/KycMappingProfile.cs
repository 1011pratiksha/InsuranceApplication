
using AutoMapper;
using KycVerification.Application.DTO;
using KycVerification.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KycVerification.Application.Mapper
{
    public class KycMappingProfile : Profile
    {
        public KycMappingProfile()
        {
            // Map incoming KYC requests to database entities.
            CreateMap<CreateCustomerKycDto, CustomerKyc>();
            CreateMap<CustomerKyc, CustomerKycDto>();

            // Map document upload requests and responses.
            CreateMap<UploadKycDocumentDto, KycDocument>();
            CreateMap<KycDocument, KycDocumentDto>();

            // Map CKYC information.
            CreateMap<CkycDetails, CkycDetailsDto>();
            CreateMap<CkycDetailsDto, CkycDetails>();

            // Map verification logs.
            CreateMap<KycVerificationLog, KycVerificationLogDto>();
            CreateMap<KycVerificationLogDto, KycVerificationLog>();

            // Map document type information.
            CreateMap<DocumentTypeMaster, DocumentTypeMasterDto>();
            CreateMap<DocumentTypeMasterDto, DocumentTypeMaster>();
        }
    }
}
