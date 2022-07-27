using TestMDM.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace TestMDM.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TestMDMPageModel : AbpPageModel
{
    protected TestMDMPageModel()
    {
        LocalizationResourceType = typeof(TestMDMResource);
        ObjectMapperContext = typeof(TestMDMWebModule);
    }
}
