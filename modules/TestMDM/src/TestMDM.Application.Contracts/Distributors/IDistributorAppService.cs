using TestMDM.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TestMDM.Distributors
{
    public interface IDistributorsAppService : IApplicationService
    {
        Task<PagedResultDto<DistributorWithNavigationPropertiesDto>> GetListAsync(GetDistributorsInput input);

        Task<DistributorWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<DistributorDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DistributorDto> CreateAsync(DistributorCreateDto input);

        Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input);
    }
}