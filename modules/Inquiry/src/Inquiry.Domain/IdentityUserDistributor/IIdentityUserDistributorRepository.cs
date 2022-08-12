using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using TestMDM.Distributors;

namespace Inquiry.IdentityUserDistributor
{
    public interface IIdentityUserDistributorRepository : IRepository<IdentityUser, Guid>
    {
        Task<Distributor> GetWithNavigationPropertiesAsync
            (
                Guid id,
                Guid distributorId,
                CancellationToken cancellationToken = default
            );

        Task<List<Distributor>> GetDistributorListAsync
            (
                Guid id,
                string filterText = null,
                string companyName = null,
                string taxId = null,
                Guid? distributorId = null,
                string sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
            );

        Task<List<Distributor>> GetListAsync
            (
                string filterText = null,
                string companyName = null,
                string taxId = null,
                string sorting = null,
                int maxResultCount = int.MaxValue,
                int skipCount = 0,
                CancellationToken cancellationToken = default
            );

        Task<long> GetCountAsync
            (
                string filterText = null,
                string companyName = null,
                string taxId = null,
                Guid? identityUserId = null,
                CancellationToken cancellationToken = default
            );
    }
}
