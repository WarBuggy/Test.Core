using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using testmdm.Distributors;
using testmdm.Shared;

namespace testmdm.Web.Pages.testmdm.Distributors
{
    public class IndexModel : AbpPageModel
    {
        public string NameFilter { get; set; }
        public string TaxIdFilter { get; set; }

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