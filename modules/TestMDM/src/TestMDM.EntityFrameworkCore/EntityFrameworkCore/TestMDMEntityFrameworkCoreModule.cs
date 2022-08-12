using TestMDM.Distributors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace TestMDM.EntityFrameworkCore;

[DependsOn(
    typeof(TestMDMDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class TestMDMEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<TestMDMDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<Distributor, EfCoreDistributorRepository>();
            options.AddRepository<IdentityUser, EfCoreIdentityRoleRepository>();
        });
    }
}