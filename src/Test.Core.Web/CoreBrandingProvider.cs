using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Test.Core.Web;

[Dependency(ReplaceServices = true)]
public class CoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Core";
}
