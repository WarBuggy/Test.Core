using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Test.Core.Distributors;
using Test.Core.Shared;

namespace Test.Core.Web.Pages.Distributors
{
    public class IndexModel : AbpPageModel
    {
        public string CompanyNameFilter { get; set; }
        public string TaxIDFilter { get; set; }

        private readonly IDistributorsAppService _distributorsAppService;

        public IndexModel(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}