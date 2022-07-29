using Volo.Abp.Reflection;

namespace TestMDM.Permissions;

public class TestMDMPermissions
{
    public const string GroupName = "TestMDM";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TestMDMPermissions));
    }

    public class Distributors
    {
        public const string Default = GroupName + ".Distributors";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}