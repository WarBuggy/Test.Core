using Inquiry.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Inquiry.Distributors;

namespace Inquiry.InquiryUses
{
    public interface IInquiryUsersAppService : IApplicationService
    {
        Task<ListResultDto<DistributorDto>> GetListDistributorDtoAsync(Guid id);
    }
}