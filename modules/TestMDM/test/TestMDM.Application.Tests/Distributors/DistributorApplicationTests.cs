using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace TestMDM.Distributors
{
    public class DistributorsAppServiceTests : testmdmApplicationTestBase
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
            result.Items.Any(x => x.Id == Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("0aee4c70-daf7-4d18-bbff-f76016531eb4")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _distributorsAppService.GetAsync(Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DistributorCreateDto
            {
                Name = "7",
                TaxId = "2"
            };

            // Act
            var serviceResult = await _distributorsAppService.CreateAsync(input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("7");
            result.TaxId.ShouldBe("2");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DistributorUpdateDto()
            {
                Name = "e",
                TaxId = "e"
            };

            // Act
            var serviceResult = await _distributorsAppService.UpdateAsync(Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"), input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Name.ShouldBe("e");
            result.TaxId.ShouldBe("e");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _distributorsAppService.DeleteAsync(Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"));

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == Guid.Parse("1501a60c-d73e-4911-b067-0b2a7cfdb750"));

            result.ShouldBeNull();
        }
    }
}