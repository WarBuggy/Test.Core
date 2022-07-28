using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Test.Core.EntityFrameworkCore;

namespace Test.Core.Distributors
{
    public class EfCoreDistributorRepository : EfCoreRepository<CoreDbContext, Distributor, Guid>, IDistributorRepository
    {
        public EfCoreDistributorRepository(IDbContextProvider<CoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Distributor>> GetListAsync(
            string filterText = null,
            string companyName = null,
            string taxID = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, companyName, taxID);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistributorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            string taxID = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, companyName, taxID);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Distributor> ApplyFilter(
            IQueryable<Distributor> query,
            string filterText,
            string companyName = null,
            string taxID = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CompanyName.Contains(filterText) || e.TaxID.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(companyName), e => e.CompanyName.Contains(companyName))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxID), e => e.TaxID.Contains(taxID));
        }
    }
}