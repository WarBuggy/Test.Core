using Inquiry.InquiryUses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Security.Claims;
using TestMDM.Distributors;
using Volo.Abp.Users;
using Inquiry.Distributors;
using Volo.Abp.AspNetCore.Authentication.OAuth;
using Volo.Abp.Modularity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;

namespace Test.Core.Web.Components.Toolbar.DistributorSelector;

public class DistributorSelectorViewComponent : AbpViewComponent
{
    private readonly IInquiryUsersAppService _inquiryUsersAppService;
    private readonly ICurrentUser _currentUser;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DistributorSelectorViewComponent(IInquiryUsersAppService inquiryUsersAppService, ICurrentUser currentUser, IHttpContextAccessor httpContextAccessor)
    {
        _inquiryUsersAppService = inquiryUsersAppService;
        _currentUser = currentUser;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!_currentUser.IsAuthenticated)
        {
            _httpContextAccessor.HttpContext.Response.Redirect("/Account/Logout");
            return Content(string.Empty);
        }
        if (_currentUser.UserName == "admin" || _currentUser.TenantId == null)
        {
            return Content(string.Empty);
        }

        var items = await GetItemsAsync(_currentUser.Id.Value);
        if (items == null || items.Count < 1)
        {
            _httpContextAccessor.HttpContext.Response.Redirect("/Account/Logout");
            return Content(string.Empty);
        }
        return View("~/Components/Toolbar/DistributorSelector/Default.cshtml", items);
    }

    private async Task<IReadOnlyList<DistributorSelectorDto>> GetItemsAsync(Guid userId)
    {
        IReadOnlyList<DistributorSelectorDto> result = new List<DistributorSelectorDto>();
        var distributorSelector = await _inquiryUsersAppService.GetListDistributorIdentityUserAsync(userId);
        if (distributorSelector != null && distributorSelector.Items.Count > 0)
        {
            result = distributorSelector.Items;
        }
        return result;
    }
}