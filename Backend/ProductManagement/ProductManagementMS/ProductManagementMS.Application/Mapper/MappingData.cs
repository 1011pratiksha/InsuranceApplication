
using AutoMapper;
using ProductManagementMS.Application.DTO;
using ProductManagementMS.Domain.Entities;

namespace ProductManagementMS.Application.Mapper
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<product_category, product_categoryDTO>().ReverseMap();

            CreateMap<insurance_product, insurance_productDTO>().ReverseMap();

            CreateMap<coverage_master, coverage_masterDTO>().ReverseMap();

            CreateMap<rider_master, rider_masterDTO>().ReverseMap();

            CreateMap<benefit_master, benefit_masterDTO>().ReverseMap();

            CreateMap<premium_frequency, premium_frequencyDTO>().ReverseMap();

            CreateMap<product_commission, product_commissionDTO>().ReverseMap();
        }
    }
}
