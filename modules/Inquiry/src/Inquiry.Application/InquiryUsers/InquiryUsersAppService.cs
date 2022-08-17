using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Inquiry.InquiryUsers;
using Inquiry.Distributors;
using Volo.Abp.ObjectMapping;

namespace Inquiry.InquiryUses
{
    //[RequiresFeature(TestMDMFeatures.Enable)]
    //[Authorize(TestMDMPermissions.Distributors.Default)]
    public class InquiryUsersAppService : ApplicationService, IInquiryUsersAppService
    {
        private readonly IInquiryUserRepository _inquiryUserRepository;

        public InquiryUsersAppService(IInquiryUserRepository inquiryUserRepository)
        {
            _inquiryUserRepository = inquiryUserRepository;
        }

        public async Task<ListResultDto<DistributorDto>> GetListDistributorDtoAsync(Guid id)
        {
            var items = await _inquiryUserRepository.GetListDistributorAsync(id);
            return new ListResultDto<DistributorDto>
                {
                    Items = ObjectMapper.Map<List<Distributor>, List<DistributorDto>>(items),
                };
        }

        //public virtual async Task<PagedResultDto<DistributorWithNavPropertiesDto>> GetListAsync(GetDistributorsInput input)
        //{
        //    var totalCount = await _distributorRepository.GetCountAsync(input.FilterText, input.CompanyName, input.TaxId, input.IdentityUserId);
        //    var items = await _distributorRepository.GetListWithNavPropertiesAsync(input.FilterText, input.CompanyName, input.TaxId, input.IdentityUserId, input.Sorting, input.MaxResultCount, input.SkipCount);

        //    return new PagedResultDto<DistributorWithNavPropertiesDto>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<DistributorWithNavProperties>, List<DistributorWithNavPropertiesDto>>(items)
        //    };
        //}

        //public virtual async Task<DistributorWithNavPropertiesDto> GetWithNavPropertiesAsync(Guid id)
        //{
        //    return ObjectMapper.Map<DistributorWithNavProperties, DistributorWithNavPropertiesDto>
        //        (await _distributorRepository.GetWithNavPropertiesAsync(id));
        //}

        //public virtual async Task<DistributorDto> GetAsync(Guid id)
        //{
        //    return ObjectMapper.Map<Distributor, DistributorDto>(await _distributorRepository.GetAsync(id));
        //}

        //public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input)
        //{
        //    var query = (await _identityUserRepository.GetQueryableAsync())
        //        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
        //            x => x.Name != null &&
        //                 x.Name.Contains(input.Filter));

        //    var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<IdentityUser>();
        //    var totalCount = query.Count();
        //    return new PagedResultDto<LookupDto<Guid>>
        //    {
        //        TotalCount = totalCount,
        //        Items = ObjectMapper.Map<List<IdentityUser>, List<LookupDto<Guid>>>(lookupData)
        //    };
        //}

        //[Authorize(TestMDMPermissions.Distributors.Delete)]
        //public virtual async Task DeleteAsync(Guid id)
        //{
        //    await _distributorRepository.DeleteAsync(id);
        //}

        //[Authorize(TestMDMPermissions.Distributors.Create)]
        //public virtual async Task<DistributorDto> CreateAsync(DistributorCreateDto input)
        //{

        //    var distributor = await _distributorManager.CreateAsync(
        //    input.IdentityUserIds, input.CompanyName, input.TaxId
        //    );

        //    return ObjectMapper.Map<Distributor, DistributorDto>(distributor);
        //}

        //[Authorize(TestMDMPermissions.Distributors.Edit)]
        //public virtual async Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input)
        //{

        //    var distributor = await _distributorManager.UpdateAsync(
        //    id,
        //    input.IdentityUserIds, input.CompanyName, input.TaxId, input.ConcurrencyStamp
        //    );

        //    return ObjectMapper.Map<Distributor, DistributorDto>(distributor);
        //}
    }
}