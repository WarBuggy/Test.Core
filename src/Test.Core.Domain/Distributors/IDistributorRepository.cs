using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Test.Core.Distributors
{
    public interface IDistributorRepository : IRepository<Distributor, Guid>
    {
        Task<List<Distributor>> GetListAsync(
            string filterText = null,
            string companyName = null,
            string taxID = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            string taxID = null,
            CancellationToken cancellationToken = default);
    }
}