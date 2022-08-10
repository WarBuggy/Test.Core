using System;
using TestMDM.Shared;
using Volo.Abp.AutoMapper;
using TestMDM.Distributors;
using AutoMapper;
using Volo.Abp.Identity;

namespace TestMDM;

public class TestMDMApplicationAutoMapperProfile : Profile
{
    public TestMDMApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Distributor, DistributorDto>();

        CreateMap<DistributorWithNavigationProperties, DistributorWithNavigationPropertiesDto>();
        CreateMap<IdentityUser, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}