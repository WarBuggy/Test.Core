using Test.Core.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Test.Core.Web.Pages;

/* Inherit your Page Model classes from this class.
 */
public abstract class CorePageModel : AbpPageModel
{
    protected CorePageModel()
    {
        LocalizationResourceType = typeof(CoreResource);
    }
}
