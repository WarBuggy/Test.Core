using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace TestMDM;

[DependsOn(
    typeof(TestMDMApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TestMDMHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TestMDMApplicationContractsModule).Assembly,
            TestMDMRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TestMDMHttpApiClientModule>();
        });
    }
}
