using AutoMapper;
using Inquiry.Distributors;
using Inquiry.Shared;
using System;
using Volo.Abp.Identity;

namespace Inquiry;

public class InquiryApplicationAutoMapperProfile : Profile
{
    public InquiryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Distributor, DistributorDto>();

        CreateMap<DistributorWithNavProperties, DistributorWithNavPropertiesDto>();
        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}