using Volo.Abp.AutoMapper;
using TestMDM.Distributors;
using AutoMapper;

namespace TestMDM.Web;

public class TestMDMWebAutoMapperProfile : Profile
{
    public TestMDMWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<DistributorDto, DistributorUpdateDto>();

        CreateMap<DistributorDto, DistributorUpdateDto>().Ignore(x => x.IdentityUserIds);
    }
}