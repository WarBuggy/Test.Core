using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace TestMDM.Distributors
{
    public class DistributorsAppServiceTests : TestMDMApplicationTestBase
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
            result.Items.Any(x => x.Id == Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("73e3ef58-8913-433d-a6a3-2770d49636b7")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _distributorsAppService.GetAsync(Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DistributorCreateDto
            {
                CompanyName = "b",
                TaxId = "2"
            };

            // Act
            var serviceResult = await _distributorsAppService.CreateAsync(input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("b");
            result.TaxId.ShouldBe("2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DistributorUpdateDto()
            {
                CompanyName = "e",
                TaxId = "e"
            };

            // Act
            var serviceResult = await _distributorsAppService.UpdateAsync(Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"), input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("e");
            result.TaxId.ShouldBe("e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _distributorsAppService.DeleteAsync(Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"));

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == Guid.Parse("272200ff-4935-47e8-b9e7-0d71220f724f"));

            result.ShouldBeNull();
        }
    }
}