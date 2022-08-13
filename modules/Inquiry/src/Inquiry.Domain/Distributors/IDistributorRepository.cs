using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inquiry.Distributors
{
    public interface IDistributorRepository : IRepository<Distributor, Guid>
    {
        Task<DistributorWithNavProperties> GetWithNavPropertiesAsync(
            Guid id,
            CancellationToken cancellationToken = default
        );

        Task<List<DistributorWithNavProperties>> GetListWithNavPropertiesAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<Distributor>> GetListAsync(
                    string filterText = null,
                    string companyName = null,
                    string taxId = null,
                    string sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default);
    }
}