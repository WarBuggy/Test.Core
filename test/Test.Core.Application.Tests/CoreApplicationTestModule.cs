using Volo.Abp.Modularity;

namespace Test.Core;

[DependsOn(
    typeof(CoreApplicationModule),
    typeof(CoreDomainTestModule)
    )]
public class CoreApplicationTestModule : AbpModule
{

}
