using Localization.Resources.AbpUi;
using TestMDM.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace TestMDM;

[DependsOn(
    typeof(TestMDMApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class TestMDMHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(TestMDMHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<TestMDMResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
