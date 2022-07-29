using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestMDM.Distributors;
using TestMDM.EntityFrameworkCore;
using Xunit;

namespace TestMDM.Distributors
{
    public class DistributorRepositoryTests : testmdmEntityFrameworkCoreTestBase
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
                    name: "8",
                    taxId: "3"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"));
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
                    name: "7",
                    taxId: "b"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}