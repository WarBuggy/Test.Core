using Inquiry.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inquiry.Distributors
{
    public interface IDistributorsAppService : IApplicationService
    {
        Task<PagedResultDto<DistributorWithNavPropertiesDto>> GetListAsync(GetDistributorsInput input);

        Task<DistributorWithNavPropertiesDto> GetWithNavPropertiesAsync(Guid id);

        Task<DistributorDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetIdentityUserLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<DistributorDto> CreateAsync(DistributorCreateDto input);

        Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input);
    }
}