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

        public DistributorManager(IDistributorRepository distributorRepository)
        {
            _distributorRepository = distributorRepository;
        }

        public async Task<Distributor> CreateAsync(
        string name, string taxId)
        {
            var distributor = new Distributor(
             GuidGenerator.Create(),
             name, taxId
             );

            return await _distributorRepository.InsertAsync(distributor);
        }

        public async Task<Distributor> UpdateAsync(
            Guid id,
            string name, string taxId, [CanBeNull] string concurrencyStamp = null
        )
        {
            var queryable = await _distributorRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var distributor = await AsyncExecuter.FirstOrDefaultAsync(query);

            distributor.Name = name;
            distributor.TaxId = taxId;

            distributor.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _distributorRepository.UpdateAsync(distributor);
        }

    }
}