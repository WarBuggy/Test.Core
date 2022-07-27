using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;

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
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            TestMDMMenus.Prefix,
            displayName: "TestMDM",
            "~/TestMDM",
            icon: "fa fa-globe");

        //Add main menu items.
        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
}