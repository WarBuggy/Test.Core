using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace TestMDM;

[DependsOn(
    typeof(TestMDMDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class TestMDMApplicationContractsModule : AbpModule
{

}
