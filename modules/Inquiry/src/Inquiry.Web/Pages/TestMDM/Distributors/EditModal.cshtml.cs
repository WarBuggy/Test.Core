using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inquiry.Distributors;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace Inquiry.Web.Pages.Distributors
{
    public class EditModalModel : InquiryPageModel
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
            var distributorWithNavigationPropertiesDto = await _distributorsAppService.GetWithNavPropertiesAsync(Id);
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