using Volo.Abp.Reflection;

namespace TestMDM.Permissions;

public class TestMDMPermissions
{
    public const string GroupName = "TestMDM";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TestMDMPermissions));
    }
}
