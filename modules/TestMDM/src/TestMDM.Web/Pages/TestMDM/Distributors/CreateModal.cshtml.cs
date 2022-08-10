using TestMDM.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMDM.Distributors;

namespace TestMDM.Web.Pages.TestMDM.Distributors
{
    public class CreateModalModel : TestMDMPageModel
    {
        [BindProperty]
        public DistributorCreateDto Distributor { get; set; }

        [BindProperty]
        public List<Guid> SelectedIdentityUserIds { get; set; }

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

            Distributor.IdentityUserIds = SelectedIdentityUserIds;

            await _distributorsAppService.CreateAsync(Distributor);
            return NoContent();
        }
    }
}