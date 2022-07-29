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
    public class EfCoreDistributorRepository : EfCoreRepository<TestMDMDbContext, Distributor, Guid>, IDistributorRepository
    {
        public EfCoreDistributorRepository(IDbContextProvider<TestMDMDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Distributor>> GetListAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, companyName, taxId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistributorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, companyName, taxId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Distributor> ApplyFilter(
            IQueryable<Distributor> query,
            string filterText,
            string companyName = null,
            string taxId = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.CompanyName.Contains(filterText) || e.TaxId.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(companyName), e => e.CompanyName.Contains(companyName))
                    .WhereIf(!string.IsNullOrWhiteSpace(taxId), e => e.TaxId.Contains(taxId));
        }
    }
}