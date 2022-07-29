using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using TestMDM.EntityFrameworkCore;

namespace TestMDM.Distributors
{
    public class EfCoreDistributorRepository : EfCoreRepository<testmdmDbContext, Distributor, Guid>, IDistributorRepository
    {
        public EfCoreDistributorRepository(IDbContextProvider<testmdmDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Distributor>> GetListAsync(
            string filterText = null,
            string name = null,
            string taxId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, taxId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistributorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string taxId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, taxId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Distributor> ApplyFilter(
            IQueryable<Distributor> query,
            string filterText,
            string name = null,
            string taxId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.TaxId.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxId), e => e.TaxId.Contains(taxId));
        }
    }
}