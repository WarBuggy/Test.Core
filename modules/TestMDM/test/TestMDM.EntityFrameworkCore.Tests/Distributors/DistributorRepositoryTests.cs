using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestMDM.Distributors;
using TestMDM.EntityFrameworkCore;
using Xunit;

namespace TestMDM.Distributors
{
    public class DistributorRepositoryTests : TestMDMEntityFrameworkCoreTestBase
    {
        private readonly IDistributorRepository _distributorRepository;

        public DistributorRepositoryTests()
        {
            _distributorRepository = GetRequiredService<IDistributorRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _distributorRepository.GetListAsync(
                    companyName: "6",
                    taxId: "f"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _distributorRepository.GetCountAsync(
                    companyName: "8",
                    taxId: "7"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}