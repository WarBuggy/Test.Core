using Volo.Abp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Inquiry.EntityFrameworkCore;

namespace Inquiry.Distributors
{
    public class EfCoreDistributorRepository : EfCoreRepository<InquiryDbContext, Distributor, Guid>, IDistributorRepository
    {
        public EfCoreDistributorRepository(IDbContextProvider<InquiryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<DistributorWithNavProperties> GetWithNavPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.IdentityUsers)
                .Select(distributor => new DistributorWithNavProperties
                {
                    Distributor = distributor,
                    IdentityUsers = (from distributorIdentityUsers in distributor.IdentityUsers
                                     join _identityUser in dbContext.Set<IdentityUser>() on distributorIdentityUsers.IdentityUserId equals _identityUser.Id
                                     select _identityUser).ToList()
                }).FirstOrDefault();
        }

        public async Task<List<DistributorWithNavProperties>> GetListWithNavPropertiesAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavPropertiesAsync();
            query = ApplyFilter(query, filterText, companyName, taxId, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestMDM.Distributors.DistributorConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DistributorWithNavProperties>> GetQueryForNavPropertiesAsync()
        {
            var dbContext = await GetDbContextAsync();

            return (dbContext.Set<Distributor>().Include(x => x.IdentityUsers))
                 .Select(distributor => new DistributorWithNavProperties
                 {
                     Distributor = distributor,
                     IdentityUsers = (from distributorIdentityUsers in distributor.IdentityUsers
                                      join _identityUser in dbContext.Set<IdentityUser>() on distributorIdentityUsers.IdentityUserId equals _identityUser.Id
                                      select _identityUser).ToList()
                 });
        }

        protected virtual IQueryable<DistributorWithNavProperties> ApplyFilter(
            IQueryable<DistributorWithNavProperties> query,
            string filterText,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null)
        {
            return query
                .Where(e => !e.Distributor.IsDeleted)
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Distributor.CompanyName.Contains(filterText) || e.Distributor.TaxId.Contains(filterText))
                .WhereIf(!string.IsNullOrWhiteSpace(companyName), e => e.Distributor.CompanyName.Contains(companyName))
                .WhereIf(!string.IsNullOrWhiteSpace(taxId), e => e.Distributor.TaxId.Contains(taxId))
                .WhereIf(identityUserId != null && identityUserId != Guid.Empty, e => e.Distributor.IdentityUsers.Any(x => x.IdentityUserId == identityUserId));
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? TestMDM.Distributors.DistributorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavPropertiesAsync();
            query = ApplyFilter(query, filterText, companyName, taxId, identityUserId);
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