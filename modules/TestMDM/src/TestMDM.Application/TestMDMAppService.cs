using TestMDM.Localization;
using Volo.Abp.Application.Services;

namespace TestMDM;

public abstract class TestMDMAppService : ApplicationService
{
    protected TestMDMAppService()
    {
        LocalizationResource = typeof(TestMDMResource);
        ObjectMapperContext = typeof(TestMDMApplicationModule);
    }
}
