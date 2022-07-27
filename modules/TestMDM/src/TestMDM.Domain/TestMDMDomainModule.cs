using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace TestMDM;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TestMDMDomainSharedModule)
)]
public class TestMDMDomainModule : AbpModule
{

}
