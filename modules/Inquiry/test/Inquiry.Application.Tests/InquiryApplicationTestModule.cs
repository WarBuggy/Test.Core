using Volo.Abp.Modularity;

namespace Inquiry;

[DependsOn(
    typeof(InquiryApplicationModule),
    typeof(InquiryDomainTestModule)
    )]
public class InquiryApplicationTestModule : AbpModule
{

}
