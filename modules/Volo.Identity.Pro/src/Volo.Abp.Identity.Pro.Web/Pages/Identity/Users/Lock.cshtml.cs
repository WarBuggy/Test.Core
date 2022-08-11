﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Volo.Abp.Identity.Web.Pages.Identity.Users;

public class Lock : IdentityPageModel
{
    [BindProperty]
    public LockViewModel LockInput { get; set; }

    public string UserName { get; set; }

    protected IIdentityUserAppService IdentityUserAppService { get; }

    public Lock(IIdentityUserAppService identityUserAppService)
    {
        IdentityUserAppService = identityUserAppService;
    }

    public virtual async Task OnGetAsync(Guid id)
    {
        var user = await IdentityUserAppService.GetAsync(id);
        UserName = user.UserName;
        LockInput = new LockViewModel
        {
            Id = id,
            LockoutEnd = user.LockoutEnd ?? DateTime.MinValue
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        ValidateModel();

        await IdentityUserAppService.LockAsync(LockInput.Id, LockInput.LockoutEnd);

        return NoContent();
    }

    public class LockViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        public DateTime LockoutEnd { get; set; }
    }
}
