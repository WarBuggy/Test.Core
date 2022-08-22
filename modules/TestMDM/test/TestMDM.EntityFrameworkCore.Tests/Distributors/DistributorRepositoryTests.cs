using Inquiry.Distributors;
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
                    companyName: "c96586542036410994e086d43adccaebb44a2648ddca4aecb22f61a998dee573f268a21eadcd4671b516d8ec218f15c3acc67bd1a1ff4fce8abab0114e9b250fb3db185a8e3042caaaaa97cd25e31500e3f2d9494b0a40ee9857824907f5dc8c640277ec79b146f382801dcf59ca84e9f1629214b42a4739ae8737135a441d229388ff405fb54a93bd141cdeb3f9327bb85e79a35f92",
                    taxId: "08d3e5a029244ef397820a586a264a644e35ed2df7e049fc8a"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("a45f1928-f55e-4827-bbd2-e5c63704c58c"));
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
                    companyName: "67e4852dced745fc91a2a67aa098488ab0c3955a49ae47118587c831276ddecba8a1b01211d8458a9aa469630d053ec3ec65edc2147642af8bbd30066df4e74513581de8a9004183befbc0e9e00f3cb5d4954d915b3a437e93e007959cb61d2feef3fa9fb4a048189c3fc087ad85689acee1917784674742ad64b478050e0ffee4b0f55ae7e842a5867732172db097b9f1dc9173811c",
                    taxId: "0cb0814e0898428abe951d47d4805e0b555be4682c8747ba89"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}