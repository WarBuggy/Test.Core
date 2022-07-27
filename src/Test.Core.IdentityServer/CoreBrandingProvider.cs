using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Test.Core;

[Dependency(ReplaceServices = true)]
public class CoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Core";
}
