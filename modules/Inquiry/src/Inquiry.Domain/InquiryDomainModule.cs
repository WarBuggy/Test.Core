using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Inquiry;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(InquiryDomainSharedModule)
)]
public class InquiryDomainModule : AbpModule
{

}
