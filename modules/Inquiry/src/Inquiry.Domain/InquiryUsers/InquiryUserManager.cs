using Volo.Abp.Domain.Services;

namespace Inquiry.InquiryUsers
{
    public class InquiryUserManager : DomainService
    {
        private readonly IInquiryUserRepository _inquiryUserRepository;

        public InquiryUserManager(IInquiryUserRepository inquiryUserRepository)
        {
            _inquiryUserRepository = inquiryUserRepository;
        }
    }
}