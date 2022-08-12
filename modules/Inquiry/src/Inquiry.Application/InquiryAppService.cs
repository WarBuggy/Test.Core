using Inquiry.Localization;
using Volo.Abp.Application.Services;

namespace Inquiry;

public abstract class InquiryAppService : ApplicationService
{
    protected InquiryAppService()
    {
        LocalizationResource = typeof(InquiryResource);
        ObjectMapperContext = typeof(InquiryApplicationModule);
    }
}
