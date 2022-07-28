using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using Test.Core.Distributors;

namespace Test.Core.Distributors
{
    public class DistributorsDataSeedContributor : IDataSeedContributor, ISingletonDependency
    {
        private bool IsSeeded = false;
        private readonly IDistributorRepository _distributorRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DistributorsDataSeedContributor(IDistributorRepository distributorRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _distributorRepository = distributorRepository;
            _unitOfWorkManager = unitOfWorkManager;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (IsSeeded)
            {
                return;
            }

            await _distributorRepository.InsertAsync(new Distributor
            (
                id: Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"),
                companyName: "9",
                taxID: "2"
            ));

            await _distributorRepository.InsertAsync(new Distributor
            (
                id: Guid.Parse("fc1ceccd-4d10-43d6-a940-833eecba1d78"),
                companyName: "0",
                taxID: "2"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}