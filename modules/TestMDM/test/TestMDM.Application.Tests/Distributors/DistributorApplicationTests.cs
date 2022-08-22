using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;
using Inquiry.Distributors;

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
            result.Items.Any(x => x.Distributor.Id == Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c")).ShouldBe(true);
            result.Items.Any(x => x.Distributor.Id == Guid.Parse("5b636648-472a-412c-bd87-cd385268ca71")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _distributorsAppService.GetAsync(Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DistributorCreateDto
            {
                CompanyName = "0913b47523de40d2ae503289490fbb796613276e858844d6a9efee9b5555571ecbe7a9b25da84af7b187cdad5fd4c2bc0a940cb4e83f41d9969246f16ea934a67a8a2551302d44ac8858e0122a9f9b17a26bda620a574810aa39f06dff50a79b6ce26bcdcbc84bc785e350aa1d7232997769a7628c974f329587bee997acc42eb9fde5d5b7494522a506f95b61da930f726dce6665bf",
                TaxId = "230954108aef47fbabeaa6b9f4e7b05dd2ec5523df2e43719f"
            };

            // Act
            var serviceResult = await _distributorsAppService.CreateAsync(input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("0913b47523de40d2ae503289490fbb796613276e858844d6a9efee9b5555571ecbe7a9b25da84af7b187cdad5fd4c2bc0a940cb4e83f41d9969246f16ea934a67a8a2551302d44ac8858e0122a9f9b17a26bda620a574810aa39f06dff50a79b6ce26bcdcbc84bc785e350aa1d7232997769a7628c974f329587bee997acc42eb9fde5d5b7494522a506f95b61da930f726dce6665bf");
            result.TaxId.ShouldBe("230954108aef47fbabeaa6b9f4e7b05dd2ec5523df2e43719f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DistributorUpdateDto()
            {
                CompanyName = "4d7c358ecc164693ae8bbc03137c1b600d4d81e90f7a4887a4677658c8d3c1a0ace9e2e07ae44f3c9522276ccc3453da6b5bdf7be4894f0cab3dd55cab508872136bc5088b894e3eabf9bf9d2a682cdf28e72787c67247b38686577e8015c0919e65fb99e08e45d3bf3fadbc4238f366425922628ff647cb898318313e4b81684cc10d4be96046518053706d3f8dc7c080b1719d3754",
                TaxId = "49d88dbf6f9547dc8a4ad4d90d833886eae5cbb469524ab6b8"
            };

            // Act
            var serviceResult = await _distributorsAppService.UpdateAsync(Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c"), input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("4d7c358ecc164693ae8bbc03137c1b600d4d81e90f7a4887a4677658c8d3c1a0ace9e2e07ae44f3c9522276ccc3453da6b5bdf7be4894f0cab3dd55cab508872136bc5088b894e3eabf9bf9d2a682cdf28e72787c67247b38686577e8015c0919e65fb99e08e45d3bf3fadbc4238f366425922628ff647cb898318313e4b81684cc10d4be96046518053706d3f8dc7c080b1719d3754");
            result.TaxId.ShouldBe("49d88dbf6f9547dc8a4ad4d90d833886eae5cbb469524ab6b8");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _distributorsAppService.DeleteAsync(Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c"));

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c"));

            result.ShouldBeNull();
        }
    }
}