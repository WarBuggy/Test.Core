using Inquiry.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Inquiry.Permissions;

public class InquiryPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(InquiryPermissions.GroupName, L("Permission:Inquiry"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<InquiryResource>(name);
    }
}
