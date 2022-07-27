using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace TestMDM;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TestMDMHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class TestMDMConsoleApiClientModule : AbpModule
{

}
