using Volo.Abp.Modularity;

namespace TestMDM;

[DependsOn(
    typeof(TestMDMApplicationModule),
    typeof(TestMDMDomainTestModule)
    )]
public class TestMDMApplicationTestModule : AbpModule
{

}
