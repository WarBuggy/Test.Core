using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Inquiry.Distributors;
using Volo.Abp.Application.Dtos;

namespace Inquiry.InquiryUses
{
    public interface IInquiryUsersAppService : IApplicationService
    {
        Task<ListResultDto<DistributorSelectorDto>> GetListDistributorIdentityUserAsync(Guid id);

        Task SetActiveDistributor(Guid distributorId, Guid identityUserId);
    }
}