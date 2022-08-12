using Volo.Abp.Reflection;

namespace Inquiry.Permissions;

public class InquiryPermissions
{
    public const string GroupName = "Inquiry";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InquiryPermissions));
    }
}
