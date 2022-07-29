using TestMDM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TestMDM.Permissions;

public class TestMDMPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestMDMPermissions.GroupName, L("Permission:TestMDM"));

        var distributorPermission = myGroup.AddPermission(testmdmPermissions.Distributors.Default, L("Permission:Distributors"));
        distributorPermission.AddChild(testmdmPermissions.Distributors.Create, L("Permission:Create"));
        distributorPermission.AddChild(testmdmPermissions.Distributors.Edit, L("Permission:Edit"));
        distributorPermission.AddChild(testmdmPermissions.Distributors.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestMDMResource>(name);
    }
}