using TestMDM.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TestMDM;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(TestMDMEntityFrameworkCoreTestModule)
    )]
public class TestMDMDomainTestModule : AbpModule
{

}
