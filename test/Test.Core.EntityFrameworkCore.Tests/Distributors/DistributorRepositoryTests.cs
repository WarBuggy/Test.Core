using Inquiry.Distributors;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Test.Core.Distributors;
using Test.Core.EntityFrameworkCore;
using TestMDM.Distributors;
using Xunit;

namespace Test.Core.Distributors
{
    public class DistributorRepositoryTests : CoreEntityFrameworkCoreTestBase
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
                    companyName: "9",
                    taxId: "2"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"));
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
                    companyName: "0",
                    taxId: "2"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}