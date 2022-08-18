using Inquiry.Distributors;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Inquiry.InquiryUsers
{
    public interface IInquiryUserRepository : IRepository<IdentityUser, Guid>
    {
        Task<List<DistributorSelector>> GetListDistributorIdentityUserAsync(Guid identityUserId,
                  CancellationToken cancellationToken = default);

        Task SetActiveDistributor(Guid distributorId, Guid identityUserId, 
            CancellationToken cancellation = default);
    }
}
