using Volo.Abp.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace TestMDM.Distributors
{
    public class DistributorManager : DomainService
    {
        private readonly IDistributorRepository _distributorRepository;
        private readonly IRepository<IdentityUser, Guid> _identityUserRepository;

        public DistributorManager(IDistributorRepository distributorRepository,
        IRepository<IdentityUser, Guid> identityUserRepository)
        {
            _distributorRepository = distributorRepository;
            _identityUserRepository = identityUserRepository;
        }

        public async Task<Distributor> CreateAsync(
        List<Guid> identityUserIds,
        string companyName, string taxId)
        {
            var distributor = new Distributor(
             GuidGenerator.Create(),
             companyName, taxId
             );

            await SetIdentityUsersAsync(distributor, identityUserIds);

            return await _distributorRepository.InsertAsync(distributor);
        }

        public async Task<Distributor> UpdateAsync(
            Guid id,
            List<Guid> identityUserIds,
        string companyName, string taxId, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _distributorRepository.WithDetailsAsync(x => x.IdentityUsers);
            var query = queryable.Where(x => x.Id == id);

            var distributor = await AsyncExecuter.FirstOrDefaultAsync(query);

            distributor.CompanyName = companyName;
            distributor.TaxId = taxId;

            await SetIdentityUsersAsync(distributor, identityUserIds);

            distributor.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _distributorRepository.UpdateAsync(distributor);
        }

        private async Task SetIdentityUsersAsync(Distributor distributor, List<Guid> identityUserIds)
        {
            if (identityUserIds == null || !identityUserIds.Any())
            {
                distributor.RemoveAllIdentityUsers();
                return;
            }

            var query = (await _identityUserRepository.GetQueryableAsync())
                .Where(x => identityUserIds.Contains(x.Id))
                .Select(x => x.Id);

            var identityUserIdsInDb = await AsyncExecuter.ToListAsync(query);
            if (!identityUserIdsInDb.Any())
            {
                return;
            }

            distributor.RemoveAllIdentityUsersExceptGivenIds(identityUserIdsInDb);

            foreach (var identityUserId in identityUserIdsInDb)
            {
                distributor.AddIdentityUser(identityUserId);
            }
        }

    }
}