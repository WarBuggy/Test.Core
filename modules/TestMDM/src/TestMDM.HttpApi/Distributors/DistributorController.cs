using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using TestMDM.Distributors;

namespace TestMDM.Distributors
{
    [RemoteService(Name = "testmdm")]
    [Area("testmdm")]
    [ControllerName("Distributor")]
    [Route("api/testmdm/distributors")]
    public class DistributorController : AbpController, IDistributorsAppService
    {
        private readonly IDistributorsAppService _distributorsAppService;

        public DistributorController(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<DistributorDto>> GetListAsync(GetDistributorsInput input)
        {
            return _distributorsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<DistributorDto> GetAsync(Guid id)
        {
            return _distributorsAppService.GetAsync(id);
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