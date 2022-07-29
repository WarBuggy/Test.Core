using TestMDM.Permissions;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Authorization.Permissions;
using System.Collections.Generic;
using Volo.Abp.Features;
using TestMDM.Localization;

namespace TestMDM.Web.Menus;

public class TestMDMMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); //Do not delete `moduleMenu` variable as it will be used by ABP Suite!

        AddMenuItemDistributors(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<TestMDMResource>();

        var moduleMenu = new ApplicationMenuItem(
            TestMDMMenus.Prefix,
            displayName: l["Menu:TestMDM"],
            "~/TestMDM",
            icon: "fa fa-globe")
            .RequireFeatures(TestMDMFeatures.Enable);

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }

    private static void AddMenuItemDistributors(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.testmdmMenus.Distributors,
                context.GetLocalizer<testmdmResource>()["Menu:Distributors"],
                "/testmdm/Distributors",
                icon: "fa fa-file-alt",
                requiredPermissionName: testmdmPermissions.Distributors.Default
            )
        );
    }
}