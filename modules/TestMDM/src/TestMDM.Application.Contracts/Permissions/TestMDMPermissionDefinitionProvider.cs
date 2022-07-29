using TestMDM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.Features;

namespace TestMDM.Permissions;

public class TestMDMPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TestMDMPermissions.GroupName, L("Permission:TestMDM"));

        var distributorPermission =
            myGroup.AddPermission(TestMDMPermissions.Distributors.Default, L("Permission:Distributors"))
            .RequireFeatures(true, TestMDMFeatures.Enable, TestMDMFeatures.Distributor);
        distributorPermission.AddChild(TestMDMPermissions.Distributors.Create, L("Permission:Create"))
            .RequireFeatures(true, TestMDMFeatures.Enable, TestMDMFeatures.Distributor);
        distributorPermission.AddChild(TestMDMPermissions.Distributors.Edit, L("Permission:Edit"))
            .RequireFeatures(true, TestMDMFeatures.Enable, TestMDMFeatures.Distributor);
        distributorPermission.AddChild(TestMDMPermissions.Distributors.Delete, L("Permission:Delete"))
            .RequireFeatures(true, TestMDMFeatures.Enable, TestMDMFeatures.Distributor);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TestMDMResource>(name);
    }
}