using Test.Core.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test.Core.Distributors;

namespace Test.Core.Web.Pages.Distributors
{
    public class CreateModalModel : CorePageModel
    {
        [BindProperty]
        public DistributorCreateDto Distributor { get; set; }

        private readonly IDistributorsAppService _distributorsAppService;

        public CreateModalModel(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        public async Task OnGetAsync()
        {
            Distributor = new DistributorCreateDto();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _distributorsAppService.CreateAsync(Distributor);
            return NoContent();
        }
    }
}