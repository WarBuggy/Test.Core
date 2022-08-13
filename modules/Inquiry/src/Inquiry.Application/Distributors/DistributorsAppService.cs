﻿using Volo.Abp.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using TestMDM.Permissions;
using Inquiry.Shared;
using TestMDM;
using Volo.Abp.Features;

namespace Inquiry.Distributors
{
    [RequiresFeature(TestMDMFeatures.Enable)]
    [Authorize(TestMDMPermissions.Distributors.Default)]
    public class DistributorsAppService : ApplicationService, IDistributorsAppService
    {
        private readonly IDistributorRepository _distributorRepository;
        private readonly DistributorManager _distributorManager;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;

        public DistributorsAppService(IDistributorRepository distributorRepository, DistributorManager distributorManager, IRepository<IdentityUser, Guid> identityUserRepository)
        {
            _distributorRepository = distributorRepository;
            _distributorManager = distributorManager;
            _identityUserRepository = identityUserRepository;
        }

        public virtual async Task<PagedResultDto<DistributorWithNavPropertiesDto>> GetListAsync(GetDistributorsInput input)
        {
            var totalCount = await _distributorRepository.GetCountAsync(input.FilterText, input.CompanyName, input.TaxId, input.IdentityUserId);
            var items = await _distributorRepository.GetListWithNavPropertiesAsync(input.FilterText, input.CompanyName, input.TaxId, input.IdentityUserId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<DistributorWithNavPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<DistributorWithNavProperties>, List<DistributorWithNavPropertiesDto>>(items)
            };
        }

        public virtual async Task<DistributorWithNavPropertiesDto> GetWithNavPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<DistributorWithNavProperties, DistributorWithNavPropertiesDto>
                (await _distributorRepository.GetWithNavPropertiesAsync(id));
        }

        public virtual async Task<DistributorDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Distributor, DistributorDto>(await _distributorRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        {
            var query = (await _identityUserRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<IdentityUser>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<IdentityUser>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(TestMDMPermissions.Distributors.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _distributorRepository.DeleteAsync(id);
        }

        [Authorize(TestMDMPermissions.Distributors.Create)]
        public virtual async Task<DistributorDto> CreateAsync(DistributorCreateDto input)
        {

            var distributor = await _distributorManager.CreateAsync(
            input.IdentityUserIds, input.CompanyName, input.TaxId
            );

            return ObjectMapper.Map<Distributor, DistributorDto>(distributor);
        }

        [Authorize(TestMDMPermissions.Distributors.Edit)]
        public virtual async Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input)
        {

            var distributor = await _distributorManager.UpdateAsync(
            id,
            input.IdentityUserIds, input.CompanyName, input.TaxId, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Distributor, DistributorDto>(distributor);
        }
    }
}