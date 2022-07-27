using Test.Core.Localization;
using Volo.Abp.Application.Services;

namespace Test.Core;

/* Inherit your application services from this class.
 */
public abstract class CoreAppService : ApplicationService
{
    protected CoreAppService()
    {
        LocalizationResource = typeof(CoreResource);
    }
}
