using Test.Core.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Test.Core.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CoreController : AbpControllerBase
{
    protected CoreController()
    {
        LocalizationResource = typeof(CoreResource);
    }
}
