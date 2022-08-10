using TestMDM.Shared;
using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using TestMDM.Distributors;

namespace TestMDM.Web.Pages.TestMDM.Distributors
{
    public class EditModalModel : TestMDMPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public DistributorUpdateDto Distributor { get; set; }

        public List<IdentityUserDto> IdentityUsers { get; set; }
        [BindProperty]
        public List<Guid> SelectedIdentityUserIds { get; set; }

        private readonly IDistributorsAppService _distributorsAppService;

        public EditModalModel(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        public async Task OnGetAsync()
        {
            var distributorWithNavigationPropertiesDto = await _distributorsAppService.GetWithNavigationPropertiesAsync(Id);
            Distributor = ObjectMapper.Map<DistributorDto, DistributorUpdateDto>(distributorWithNavigationPropertiesDto.Distributor);

            IdentityUsers = distributorWithNavigationPropertiesDto.IdentityUsers;

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            Distributor.IdentityUserIds = SelectedIdentityUserIds;

            await _distributorsAppService.UpdateAsync(Id, Distributor);
            return NoContent();
        }
    }
}