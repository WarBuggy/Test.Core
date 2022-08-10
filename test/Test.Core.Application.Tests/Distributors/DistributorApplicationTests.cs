using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using TestMDM.Distributors;

namespace Test.Core.Distributors
{
    public class DistributorsAppServiceTests : CoreApplicationTestBase
    {
        private readonly IDistributorsAppService _distributorsAppService;
        private readonly IRepository<Distributor, Guid> _distributorRepository;

        public DistributorsAppServiceTests()
        {
            _distributorsAppService = GetRequiredService<IDistributorsAppService>();
            _distributorRepository = GetRequiredService<IRepository<Distributor, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _distributorsAppService.GetListAsync(new GetDistributorsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Distributor.Id == Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea")).ShouldBe(true);
            result.Items.Any(x => x.Distributor.Id == Guid.Parse("fc1ceccd-4d10-43d6-a940-833eecba1d78")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _distributorsAppService.GetAsync(Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DistributorCreateDto
            {
                CompanyName = "b",
                TaxId = "4"
            };

            // Act
            var serviceResult = await _distributorsAppService.CreateAsync(input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("b");
            result.TaxId.ShouldBe("4");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DistributorUpdateDto()
            {
                CompanyName = "8",
                TaxId = "b"
            };

            // Act
            var serviceResult = await _distributorsAppService.UpdateAsync(Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"), input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("8");
            result.TaxId.ShouldBe("b");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _distributorsAppService.DeleteAsync(Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"));

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == Guid.Parse("16d3bcff-10c4-46f6-8478-c8b08f3cb7ea"));

            result.ShouldBeNull();
        }
    }
}