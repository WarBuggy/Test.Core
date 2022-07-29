using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;
using TestMDM.Distributors;

namespace TestMDM.Distributors
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
                id: Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"),
                name: "8",
                taxId: "3"
            ));

            await _distributorRepository.InsertAsync(new Distributor
            (
                id: Guid.Parse("0aee4c70-daf7-4d18-bbff-f76016531eb4"),
                name: "7",
                taxId: "b"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}