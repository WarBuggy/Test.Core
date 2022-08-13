using AutoMapper;
using Inquiry.Distributors;
using Volo.Abp.AutoMapper;

namespace Inquiry.Web;

public class InquiryWebAutoMapperProfile : Profile
{
    public InquiryWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<DistributorDto, DistributorUpdateDto>();

        CreateMap<DistributorDto, DistributorUpdateDto>().Ignore(x => x.IdentityUserIds);
    }
}
