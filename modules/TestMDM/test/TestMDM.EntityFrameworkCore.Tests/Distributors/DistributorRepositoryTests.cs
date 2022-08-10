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
                    companyName: "d7ca572f9705458a9223131d5fe0087446531b60a1f348b199ae4e507ceacb9918b478dd09344e1da3f313476574ccc17d6352e4fc79431eb4ef6de936db49468c5355676f464f1bb0a3f8067d706048b125501b70f34a7bbb643fc2af5eecb9106d1abac17e45fa8dfd4d63c257cd0a1f707dcb9b8e46dcb5f98a7509fb08e453bbb6a06297450b8a7a5ac023e883a23b54462cb7f6",
                    taxId: "c76f0f7d756b47859c47b27a2c1a162b886c66feb5b8409097"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e"));
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
                    companyName: "5d9be99b1b3b48a9a9a03b7d1f91c1b12f3dbd2b949448ac92af4186238d1dbea7127a594a754b47a0eb1e159a784baa89d1e147909444b1bd94baed0c83932f8468ac14f73a40cfb1a1b1c617e195c5f5bd1a5d0f1548faa33972b00a77853ac13992903957411fb9d8ee2d6a8dbe4b59dd24a193b541e7875fc93b321bce0efe0ea5a2e7a04e0f9c22b4e0afc6e00fce5e95ff227c",
                    taxId: "577518dcfd7845d79bb2f477434b80b055179bd0bb6f4ee9a5"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}