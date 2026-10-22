using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Domain.Entities;

namespace CustomerRegistration.Application.Mapper
{
    public class CustomerMapper : Profile
    {
        public CustomerMapper()
        {
            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(x => x.CustomerId, x => x.Ignore())
                .ForMember(x => x.UserId, x => x.Ignore())
                .ForMember(x => x.CustomerCode, x => x.Ignore())
                .ForMember(x => x.CustomerStatus, x => x.Ignore())
                .ForMember(x => x.CreatedAt, x => x.Ignore())
                .ForMember(x => x.UpdatedAt, x => x.Ignore())
                .ForMember(x => x.CustomerAddresses, x => x.Ignore())
                .ForMember(x => x.CustomerContacts, x => x.Ignore())
                .ForMember(x => x.CustomerDocuments, x => x.Ignore())
                .ForMember(x => x.Nominees, x => x.Ignore());

            CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(x => x.CustomerId, x => x.Ignore())
                .ForMember(x => x.UserId, x => x.Ignore())
                .ForMember(x => x.CustomerCode, x => x.Ignore())
                .ForMember(x => x.CustomerStatus, x => x.Ignore())
                .ForMember(x => x.CreatedAt, x => x.Ignore())
                .ForMember(x => x.UpdatedAt, x => x.Ignore())
                .ForMember(x => x.CustomerAddresses, x => x.Ignore())
                .ForMember(x => x.CustomerContacts, x => x.Ignore())
                .ForMember(x => x.CustomerDocuments, x => x.Ignore())
                .ForMember(x => x.Nominees, x => x.Ignore());

            CreateMap<Customer, CustomerResponseDto>();

            CreateMap<CustomerAddressDto, CustomerAddress>()
                .ForMember(x => x.AddressId, x => x.Ignore())
                .ForMember(x => x.CustomerId, x => x.Ignore());

            CreateMap<CustomerAddress, CustomerAddressDto>();

            CreateMap<NomineeDto, Nominee>()
                .ForMember(x => x.NomineeId, x => x.Ignore())
                .ForMember(x => x.CustomerId, x => x.Ignore());

            CreateMap<Nominee, NomineeDto>();

            CreateMap<CustomerDocument, CustomerDocumentDto>();
        }
    }
}