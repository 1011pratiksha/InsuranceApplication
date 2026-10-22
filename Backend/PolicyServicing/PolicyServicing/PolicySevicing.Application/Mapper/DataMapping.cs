using AutoMapper;
using PolicyServicing.Domain.Entities;
using PolicySevicing.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicySevicing.Application.Mapper
{
    public class DataMapping : Profile
    {
        public DataMapping()
        {
            CreateMap<ServiceRequest, ServiceRequestResponseDto>();
            CreateMap<CreateServiceRequestDto, ServiceRequest>();

            CreateMap<PolicyEndorsement, EndorsementDto2>();
            CreateMap<EndorsementDto, PolicyEndorsement>();


            CreateMap<PolicyRenewal, RenewalDto2>();
            CreateMap<RenewPolicyDto, PolicyRenewal>();

            CreateMap<CancelPolicyDto, PolicyCancellation>();
            CreateMap<PolicyCancellation, CancellationDto>();
        }
    }
}
