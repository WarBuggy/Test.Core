using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Inquiry;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(InquiryHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class InquiryConsoleApiClientModule : AbpModule
{

}
