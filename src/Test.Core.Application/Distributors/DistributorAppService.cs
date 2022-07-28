using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Test.Core.Permissions;
using Test.Core.Distributors;

namespace Test.Core.Distributors
{
    [RemoteService(IsEnabled = false)]
    [Authorize(CorePermissions.Distributors.Default)]
    public class DistributorsAppService : ApplicationService, IDistributorsAppService
    {
        private readonly IDistributorRepository _distributorRepository;
        private readonly DistributorManager _distributorManager;

        public DistributorsAppService(IDistributorRepository distributorRepository, DistributorManager distributorManager)
        {
            _distributorRepository = distributorRepository;
            _distributorManager = distributorManager;
        }

        public virtual async Task<PagedResultDto<DistributorDto>> GetListAsync(GetDistributorsInput input)
        {
            var totalCount = await _distributorRepository.GetCountAsync(input.FilterText, input.CompanyName, input.TaxID);
            var items = await _distributorRepository.GetListAsync(input.FilterText, input.CompanyName, input.TaxID, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DistributorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Distributor>, List<DistributorDto>>(items)
            };
        }

        public virtual async Task<DistributorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Distributor, DistributorDto>(await _distributorRepository.GetAsync(id));
        }

        [Authorize(CorePermissions.Distributors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _distributorRepository.DeleteAsync(id);
        }

        [Authorize(CorePermissions.Distributors.Create)]
        public virtual async Task<DistributorDto> CreateAsync(DistributorCreateDto input)
        {

            var distributor = await _distributorManager.CreateAsync(
            input.CompanyName, input.TaxID
            );

            return ObjectMapper.Map<Distributor, DistributorDto>(distributor);
        }

        [Authorize(CorePermissions.Distributors.Edit)]
        public virtual async Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input)
        {

            var distributor = await _distributorManager.UpdateAsync(
            id,
            input.CompanyName, input.TaxID, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Distributor, DistributorDto>(distributor);
        }
    }
}