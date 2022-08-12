using Inquiry.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Inquiry.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class InquiryPageModel : AbpPageModel
{
    protected InquiryPageModel()
    {
        LocalizationResourceType = typeof(InquiryResource);
        ObjectMapperContext = typeof(InquiryWebModule);
    }
}
