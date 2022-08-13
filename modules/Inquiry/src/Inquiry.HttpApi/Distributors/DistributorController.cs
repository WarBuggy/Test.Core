using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Inquiry.Shared;

namespace Inquiry.Distributors
{
    [RemoteService(Name = "Inquiry")]
    [Area("inquiry")]
    [ControllerName("Distributor")]
    [Route("api/inquiry/distributors")]
    public class DistributorController : AbpController, IDistributorsAppService
    {
        private readonly IDistributorsAppService _distributorsAppService;

        public DistributorController(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        [HttpGet]
        public Task<PagedResultDto<DistributorWithNavPropertiesDto>> GetListAsync(GetDistributorsInput input)
        {
            return _distributorsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public Task<DistributorWithNavPropertiesDto> GetWithNavPropertiesAsync(Guid id)
        {
            return _distributorsAppService.GetWithNavPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DistributorDto> GetAsync(Guid id)
        {
            return _distributorsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("identity-user-lookup")]
        public Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            return _distributorsAppService.GetIdentityUserLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<DistributorDto> CreateAsync(DistributorCreateDto input)
        {
            return _distributorsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input)
        {
            return _distributorsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _distributorsAppService.DeleteAsync(id);
        }
    }
}