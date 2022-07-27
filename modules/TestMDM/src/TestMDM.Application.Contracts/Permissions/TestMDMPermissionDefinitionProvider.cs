using TestMDM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TestMDM.Permissions;

public class TestMDMPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestMDMPermissions.GroupName, L("Permission:TestMDM"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestMDMResource>(name);
    }
}
