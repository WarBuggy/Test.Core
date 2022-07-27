using Test.Core.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Test.Core;

[DependsOn(
    typeof(CoreEntityFrameworkCoreTestModule)
    )]
public class CoreDomainTestModule : AbpModule
{

}
