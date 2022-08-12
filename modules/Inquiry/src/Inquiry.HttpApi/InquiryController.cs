using Inquiry.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Inquiry;

public abstract class InquiryController : AbpControllerBase
{
    protected InquiryController()
    {
        LocalizationResource = typeof(InquiryResource);
    }
}
