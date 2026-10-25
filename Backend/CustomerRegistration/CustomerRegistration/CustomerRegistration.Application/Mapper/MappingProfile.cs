using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Domain.Entities;

namespace CustomerRegistration.Application.Mapper
{
    public class CustomerRegistrationMapper : Profile
    {
        public CustomerRegistrationMapper()
        {
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerResponseDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>();
            CreateMap<UpdateCustomerDto, Customer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerDetailDto>().ReverseMap();
            CreateMap<Customer, RegisterCustomerDto>().ReverseMap();

            CreateMap<CustomerAddress, CustomerAddressDto>().ReverseMap();
            CreateMap<CustomerAddress, CreateAddressDto>().ReverseMap();

            CreateMap<CustomerContact, CustomerContactDto>().ReverseMap();
            CreateMap<CustomerContact, CreateContactDto>().ReverseMap();

            CreateMap<Nominee, CreateNomineeDto>().ReverseMap();
            CreateMap<Nominee, NomineeResponseDto>().ReverseMap();
            CreateMap<Nominee, NomineeDto>().ReverseMap();

            CreateMap<CustomerDocument, UploadDocumentDto>().ReverseMap();
            CreateMap<CustomerDocument, DocumentResponseDto>().ReverseMap();
            CreateMap<CustomerDocument, CustomerDocumentDto>().ReverseMap();
        }
    }
}
