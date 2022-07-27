using TestMDM.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TestMDM;

public abstract class TestMDMController : AbpControllerBase
{
    protected TestMDMController()
    {
        LocalizationResource = typeof(TestMDMResource);
    }
}
