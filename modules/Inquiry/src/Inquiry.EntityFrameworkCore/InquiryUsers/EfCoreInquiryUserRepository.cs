using Inquiry.Distributors;
using Inquiry.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Inquiry.InquiryUsers
{
    public class EfCoreInquiryUserRepository : EfCoreRepository<InquiryDbContext, IdentityUser, Guid>, IInquiryUserRepository
    {
        public EfCoreInquiryUserRepository(IDbContextProvider<InquiryDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<DistributorSelector>> GetListDistributorIdentityUserAsync(Guid identityUserId, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();
            var distributorIdentityUsers = dbContext.Set<DistributorIdentityUser>().Where(diu => diu.IdentityUserId == identityUserId);
            var distributors = dbContext.Set<Distributor>();

            return (from distributor in distributors
                    join distributorIdentityUser in distributorIdentityUsers
                    on distributor.Id equals distributorIdentityUser.DistributorId
                    select new DistributorSelector() { 
                        DistributorId = distributor.Id, 
                        IsActive = distributorIdentityUser.IsActive, 
                        CompanyName = distributor.CompanyName,
                    }).ToList();
        }

        public async Task SetActiveDistributor(Guid distributorId, Guid identityUserId, CancellationToken cancellation = default)
        {
            var dbContext = await GetDbContextAsync();
            var distributorIdentityUsers = dbContext.Set<DistributorIdentityUser>().Where(diu => diu.IdentityUserId == identityUserId);
            distributorIdentityUsers.ToList().ForEach(diu =>
            {
                if (diu.DistributorId != distributorId)
                {
                    diu.IsActive = false;
                }
                else
                {
                    diu.IsActive = true;
                }
            });
        }
    }
}
