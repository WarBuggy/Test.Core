using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace Test.Core.Distributors
{
    public class DistributorManager : DomainService
    {
        private readonly IDistributorRepository _distributorRepository;

        public DistributorManager(IDistributorRepository distributorRepository)
        {
            _distributorRepository = distributorRepository;
        }

        public async Task<Distributor> CreateAsync(
        string companyName, string taxID)
        {
            var distributor = new Distributor(
             GuidGenerator.Create(),
             companyName, taxID
             );

            return await _distributorRepository.InsertAsync(distributor);
        }

        public async Task<Distributor> UpdateAsync(
            Guid id,
            string companyName, string taxID, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _distributorRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var distributor = await AsyncExecuter.FirstOrDefaultAsync(query);

            distributor.CompanyName = companyName;
            distributor.TaxID = taxID;

            distributor.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _distributorRepository.UpdateAsync(distributor);
        }

    }
}