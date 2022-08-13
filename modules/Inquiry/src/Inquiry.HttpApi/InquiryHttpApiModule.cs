using Localization.Resources.AbpUi;
using Inquiry.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using TestMDM.Localization;

namespace Inquiry;

[DependsOn(
    typeof(InquiryApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class InquiryHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(InquiryHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<InquiryResource>()
                .AddBaseTypes(typeof(AbpUiResource));
            options.Resources
              .Get<TestMDMResource>()
              .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
