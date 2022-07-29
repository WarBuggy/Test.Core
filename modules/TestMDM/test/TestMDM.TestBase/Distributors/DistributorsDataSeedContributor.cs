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
                id: Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"),
                companyName: "6",
                taxId: "f"
            ));

            await _distributorRepository.InsertAsync(new Distributor
            (
                id: Guid.Parse("73e3ef58-8913-433d-a6a3-2770d49636b7"),
                companyName: "8",
                taxId: "7"
            ));

            await _unitOfWorkManager.Current.SaveChangesAsync();

            IsSeeded = true;
        }
    }
}