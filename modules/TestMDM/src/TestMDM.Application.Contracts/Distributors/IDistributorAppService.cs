using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TestMDM.Distributors
{
    public interface IDistributorsAppService : IApplicationService
    {
        Task<PagedResultDto<DistributorDto>> GetListAsync(GetDistributorsInput input);

        Task<DistributorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DistributorDto> CreateAsync(DistributorCreateDto input);

        Task<DistributorDto> UpdateAsync(Guid id, DistributorUpdateDto input);
    }
}