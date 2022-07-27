﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Test.Core.Web.Pages;

public class IndexModel : CorePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
