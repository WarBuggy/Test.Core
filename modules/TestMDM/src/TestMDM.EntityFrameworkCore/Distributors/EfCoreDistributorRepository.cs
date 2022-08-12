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
using TestMDM.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace TestMDM.Distributors
{
    public class EfCoreDistributorRepository : EfCoreRepository<TestMDMDbContext, Distributor, Guid>, IDistributorRepository
    {
        public EfCoreDistributorRepository(IDbContextProvider<TestMDMDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<DistributorWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id).Include(x => x.IdentityUsers)
                .Select(distributor => new DistributorWithNavigationProperties
                {
                    Distributor = distributor,
                    IdentityUsers = (from distributorIdentityUsers in distributor.IdentityUsers
                                     join _identityUser in dbContext.Set<IdentityUser>() on distributorIdentityUsers.IdentityUserId equals _identityUser.Id
                                     select _identityUser).ToList()
                }).FirstOrDefault();
        }

        public async Task<List<DistributorWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, companyName, taxId, identityUserId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistributorConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<DistributorWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            //return from company in (await GetDbSetAsync())

            //       select new CompanyWithNavigationProperties
            //       {
            //           Company = company,
            //           IdentityUsers = new List<IdentityUser>()
            //       };

            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Include(x => x.IdentityUsers)
                 .Select(distributor => new DistributorWithNavigationProperties
                 {
                     Distributor = distributor,
                     IdentityUsers = (from distributorIdentityUsers in distributor.IdentityUsers
                                      join _identityUser in dbContext.Set<IdentityUser>() on distributorIdentityUsers.IdentityUserId equals _identityUser.Id
                                      select _identityUser).ToList()
                 });
        }

        protected virtual IQueryable<DistributorWithNavigationProperties> ApplyFilter(
            IQueryable<DistributorWithNavigationProperties> query,
            string filterText,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null)
        {
            return query
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
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? DistributorConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string companyName = null,
            string taxId = null,
            Guid? identityUserId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
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
        protected virtual IQueryable<AbpIdentityUserWithNavigationProperties> ApplyFilterAbpIdentityUser(
        IQueryable<AbpIdentityUserWithNavigationProperties> query,
        string filterText,
        Guid? roleId = null,
        Guid? organizationUnitId = null,
        string userName = null,
        string phoneNumber = null,
        string emailAddress = null,
        bool? isLockedOut = null,
        bool? notActive = null)
        {
            return query
                .WhereIf(
                !filterText.IsNullOrWhiteSpace(),
                u =>
                    u.IdentityUser.UserName.Contains(filterText) ||
                    u.IdentityUser.Email.Contains(filterText) ||
                    (u.IdentityUser.Name != null && u.IdentityUser.Name.Contains(filterText)) ||
                    (u.IdentityUser.Surname != null && u.IdentityUser.Surname.Contains(filterText)) ||
                    (u.IdentityUser.PhoneNumber != null && u.IdentityUser.PhoneNumber.Contains(filterText))
            )
            .WhereIf(roleId.HasValue, u => u.IdentityUser.Roles.Any(x => x.RoleId == roleId.Value))
            .WhereIf(organizationUnitId.HasValue, u => u.IdentityUser.OrganizationUnits.Any(x => x.OrganizationUnitId == organizationUnitId.Value))
            .WhereIf(!string.IsNullOrWhiteSpace(userName), u => u.IdentityUser.UserName == userName)
            .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), u => u.IdentityUser.PhoneNumber == phoneNumber)
            .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), u => u.IdentityUser.Email == emailAddress)
            .WhereIf(isLockedOut == true, u => u.IdentityUser.LockoutEnabled && u.IdentityUser.LockoutEnd.Value.CompareTo(DateTime.UtcNow) > 0)
            .WhereIf(notActive == true, u => !u.IdentityUser.IsActive);
        }

        public async Task<List<AbpIdentityUserWithNavigationProperties>> GetIdentityUserListWithNavigationPropertiesAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filterText = null,
            bool includeDetails = false,
            Guid? roleId = null,
            Guid? organizationUnitId = null,
            string userName = null,
            string phoneNumber = null,
            string emailAddress = null,
            bool? isLockedOut = null,
            bool? notActive = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForIdentityUserNavigationPropertiesAsync(includeDetails);
            query = ApplyFilterAbpIdentityUser(query, filterText, roleId, organizationUnitId, userName, phoneNumber, emailAddress, isLockedOut, notActive);
            query = query.OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(IdentityUser.UserName) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<AbpIdentityUserWithNavigationProperties>> GetQueryForIdentityUserNavigationPropertiesAsync(bool includeDetails)
        {
            var dbContext = await GetDbContextAsync();

            return dbContext.IdentityUsers.IncludeDetails(includeDetails)
                .Select(identityUsers => new AbpIdentityUserWithNavigationProperties
                {
                    IdentityUser = identityUsers,
                    Distributors = (from distributorIdentityUser in dbContext.DistributorIdentityUsers
                                    join distributor in dbContext.Distributors on distributorIdentityUser.DistributorId equals distributor.Id
                                    where distributorIdentityUser.IdentityUserId == identityUsers.Id
                                    select distributor).ToList(),
                });
        }
    }
}