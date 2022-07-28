using Test.Core.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Test.Core.Distributors;

namespace Test.Core.Web.Pages.Distributors
{
    public class EditModalModel : CorePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DistributorUpdateDto Distributor { get; set; }

        private readonly IDistributorsAppService _distributorsAppService;

        public EditModalModel(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        public async Task OnGetAsync()
        {
            var distributor = await _distributorsAppService.GetAsync(Id);
            Distributor = ObjectMapper.Map<DistributorDto, DistributorUpdateDto>(distributor);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _distributorsAppService.UpdateAsync(Id, Distributor);
            return NoContent();
        }
    }
}