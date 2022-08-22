using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Test.Core.Web.Components.Toolbar.DistributorSelector;
using Test.Core.Web.Components.Toolbar.Impersonation;
using Test.Core.Web.Components.Toolbar.LoginLink;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Users;

namespace Test.Core.Web.Menus;

public class CoreToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != StandardToolbars.Main)
        {
            return Task.CompletedTask;
        }

        ICurrentUser currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        if (!currentUser.IsAuthenticated)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginLinkViewComponent)));
        }
        else if (currentUser.TenantId != null)
        {
            context.Toolbar.Items.Insert(0, new ToolbarItem(typeof(DistributorSelectorViewComponent)));
        }

        if (context.ServiceProvider.GetRequiredService<ICurrentUser>().FindImpersonatorUserId() != null)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(ImpersonationViewComponent), order: -1));
        }

        return Task.CompletedTask;
    }
}