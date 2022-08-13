using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inquiry.Distributors;
using System.Collections.Generic;
using System;
using TestMDM.Web.Pages;

namespace Inquiry.Web.Pages.Distributors
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