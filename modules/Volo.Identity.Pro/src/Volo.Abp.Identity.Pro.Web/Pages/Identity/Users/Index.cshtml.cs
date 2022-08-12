﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Volo.Abp.Identity.Web.Pages.Identity.Users;

public class IndexModel : IdentityPageModel
{
    public Guid? RoleId { get; set; }

    public Guid? OrganizationUnitId { get; set; }

    [BindProperty(SupportsGet = true)]
    public string UserName { get; set; }

    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [BindProperty(SupportsGet = true)]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }

    public List<SelectListItem> RolesComboboxItems { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("", "", true)
        };

    public List<SelectListItem> OrganizationUnitsComboboxItems { get; set; } = new List<SelectListItem>
        {
            new SelectListItem("", "", true)
        };

    protected IIdentityUserAppService IdentityUserAppService { get; }

    public IndexModel(IIdentityUserAppService identityUserAppService)
    {
        IdentityUserAppService = identityUserAppService;
    }

    public virtual async Task<IActionResult> OnGetAsync()
    {
        await FillRoleSelectListAsync();
        await FillOrganizationUnitSelectListAsync();

        return Page();
    }

    public virtual Task<IActionResult> OnPostAsync()
    {
        return Task.FromResult<IActionResult>(Page());
    }

    private async Task FillRoleSelectListAsync()
    {
        var roles = await IdentityUserAppService.GetRoleLookupAsync();

        RolesComboboxItems
            .AddRange(roles.Select(role => new SelectListItem(role.Name, role.Id.ToString())));
    }

    private async Task FillOrganizationUnitSelectListAsync()
    {
        var organizationUnits = await IdentityUserAppService.GetOrganizationUnitLookupAsync();

        OrganizationUnitsComboboxItems
            .AddRange(organizationUnits.Select(organizationUnit => new SelectListItem(organizationUnit.DisplayName, organizationUnit.Id.ToString())));
    }
}
