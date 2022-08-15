using Volo.Abp.Domain.Services;

namespace Inquiry.InquiryUsers
{
    public class InquiryUserManager : DomainService
    {
        private readonly IInquiryUserRepository _inquiryUserRepository;
        //private readonly IRepository<IdentityUser, Guid> _identityUserRepository;

        public InquiryUserManager(IInquiryUserRepository inquiryUserRepository
        //,IRepository<IdentityUser, Guid> identityUserRepository
            )
        {
            _inquiryUserRepository = inquiryUserRepository;
            //_identityUserRepository = identityUserRepository;
        }
    }
}