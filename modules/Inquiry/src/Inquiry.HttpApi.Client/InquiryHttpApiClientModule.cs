using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Inquiry;

[DependsOn(
    typeof(InquiryApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class InquiryHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(InquiryApplicationContractsModule).Assembly,
            InquiryRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<InquiryHttpApiClientModule>();
        });
    }
}
