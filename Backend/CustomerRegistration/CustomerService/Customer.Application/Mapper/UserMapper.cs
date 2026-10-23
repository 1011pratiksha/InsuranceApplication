using AutoMapper;
using CustomerRegistration.Application.DTO;
using CustomerRegistration.Domain.Entities;

namespace CustomerRegistration.Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterUserDto, User>()
                .ForMember(x => x.UserId, x => x.Ignore())
                .ForMember(x => x.PasswordHash, x => x.Ignore())
                .ForMember(x => x.Role, x => x.Ignore())
                .ForMember(x => x.IsActive, x => x.Ignore())
                .ForMember(x => x.TwoFactorEnabled, x => x.Ignore())
                .ForMember(x => x.TwoFactorSecretKey, x => x.Ignore())
                .ForMember(x => x.CreatedAt, x => x.Ignore());

            CreateMap<User, UserResponseDto>();
        }
    }
}