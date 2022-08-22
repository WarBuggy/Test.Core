using Inquiry.InquiryUses;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using TestMDM.Distributors;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;
using Volo.Abp;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace Test.Core.Controllers.DistributorSelector;
//namespace Test.Core.Web.Components.Toolbar.DistributorSelector;
// namespace Test.Core
[RemoteService(Name = "DistributorSelector")]
[Area("distributor-selector")]
[ControllerName("DistributorSelector")]
[Route("api/distributors-selector")]
public class DistributorSelectorController : AbpController
{
    private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
    private readonly IdentityUserManager _userManager;
    private readonly AbpSignInManager _signInManager;
    private readonly IInquiryUsersAppService _inquiryUserService;

    public DistributorSelectorController(
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        IdentityUserManager userManager,
        AbpSignInManager signInManager,
        IInquiryUsersAppService inquiryUserService
        )
    {
        _currentPrincipalAccessor = currentPrincipalAccessor;
        _inquiryUserService = inquiryUserService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    [Route("set-current-distributor/{id}")]
    public async Task SetCurrentDistributor(string id)
    {
        var claimsIdentity = _currentPrincipalAccessor.Principal.Identities.First();
        string distributorClaimValue = claimsIdentity.FindFirst(DistributorConsts.DistributorClaimName)?.Value;
        Claim currentDistributorClaim = claimsIdentity.FindFirst(DistributorConsts.CurrentDistributorClaimName);
        string currentDistributorClaimValue = currentDistributorClaim?.Value;
        if (distributorClaimValue == null)
        {
            return;
        }
        string[] distributorIds = distributorClaimValue.Split(DistributorConsts.DistributorClaimSeparator);
        if (!distributorIds.Contains(id) || currentDistributorClaimValue == id)
        {
            return;
        }
        claimsIdentity.RemoveClaim(currentDistributorClaim);
        claimsIdentity.AddClaim(new Claim(DistributorConsts.CurrentDistributorClaimName, id));
        await _inquiryUserService.SetActiveDistributor(new Guid(id), CurrentUser.Id.Value);

        string aaa = CurrentUser.FindClaimValue("aaa");
        var user = await _userManager.FindByIdAsync(CurrentUser.Id.Value.ToString());
        await _signInManager.SignOutAsync();
        await _signInManager.SignInWithClaimsAsync(user, true, new List<Claim> { new Claim("aaa", "111") });
    }
}