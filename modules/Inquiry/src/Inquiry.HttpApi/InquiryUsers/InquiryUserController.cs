using Inquiry.Distributors;
using Inquiry.InquiryUses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Inquiry.InquiryUsers
{
    [RemoteService(Name = "Inquiry")]
    [Area("inquiry")]
    [ControllerName("InquiryUsers")]
    [Route("api/inquiry/inquiry-users")]
    public class InquiryUserController : AbpController, IInquiryUsersAppService
    {
        private readonly IInquiryUsersAppService _inquiryUsersAppService;

        public InquiryUserController(IInquiryUsersAppService inquiryUsersAppService)
        {
            _inquiryUsersAppService = inquiryUsersAppService;
        }

        [HttpGet]
        [Route("distributor-lookup/{id}")]
        public Task<ListResultDto<DistributorDto>> GetListDistributorDtoAsync(Guid id)
        {
            return _inquiryUsersAppService.GetListDistributorDtoAsync(id);
        }
    }
}
