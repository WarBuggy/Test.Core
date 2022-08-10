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
            result.Items.Any(x => x.Distributor.Id == Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e")).ShouldBe(true);
            result.Items.Any(x => x.Distributor.Id == Guid.Parse("4bfdf9bb-39ef-454b-b5e3-9fa498b757d1")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _distributorsAppService.GetAsync(Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new DistributorCreateDto
            {
                CompanyName = "1cfef8f73f4e455789197af0e0d7bb38e891cf4e43894a5da83d5580b59fc3d470fc14aa66074c0a92cbc103c7306d96f1f1c1838e0d42febb5ef0bdfb0200a1881d2f1270ef441aa88c04b28124e6dc27ec450f7619418fae54403c6d961d278247bfdba7bb47a9b3f08dba2dbc274eee3a6d8b55644d90bb978657a174f3f7d0bf01f525c9412ca3980c7f34124c4f8d87475f061b",
                TaxId = "a60dd1ed864843cd8fdc5441cc9750af7b1ad018d5a24af688"
            };

            // Act
            var serviceResult = await _distributorsAppService.CreateAsync(input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("1cfef8f73f4e455789197af0e0d7bb38e891cf4e43894a5da83d5580b59fc3d470fc14aa66074c0a92cbc103c7306d96f1f1c1838e0d42febb5ef0bdfb0200a1881d2f1270ef441aa88c04b28124e6dc27ec450f7619418fae54403c6d961d278247bfdba7bb47a9b3f08dba2dbc274eee3a6d8b55644d90bb978657a174f3f7d0bf01f525c9412ca3980c7f34124c4f8d87475f061b");
            result.TaxId.ShouldBe("a60dd1ed864843cd8fdc5441cc9750af7b1ad018d5a24af688");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new DistributorUpdateDto()
            {
                CompanyName = "522c474599b348d582d99c8e28b86cd2bdec968650214ec38ef95639950900e104f92effbad843f4a1d04156a25338ac26727063916a4bbcbd78af1d76adddb695ad456bca30480fa41044ec6b049fae3f06e115dbb04b329d497122b32bc4f323dfc6def5594260b185cc6e8daea0eb6eceea34652a442db6e670fa3b872f0809487a00c52c494095ec3bdf6c9a9e32e595ff5b3213",
                TaxId = "3cfbe0ea9fd641869788fbf092cf612b3102ebb38c684c38bb"
            };

            // Act
            var serviceResult = await _distributorsAppService.UpdateAsync(Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e"), input);

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.CompanyName.ShouldBe("522c474599b348d582d99c8e28b86cd2bdec968650214ec38ef95639950900e104f92effbad843f4a1d04156a25338ac26727063916a4bbcbd78af1d76adddb695ad456bca30480fa41044ec6b049fae3f06e115dbb04b329d497122b32bc4f323dfc6def5594260b185cc6e8daea0eb6eceea34652a442db6e670fa3b872f0809487a00c52c494095ec3bdf6c9a9e32e595ff5b3213");
            result.TaxId.ShouldBe("3cfbe0ea9fd641869788fbf092cf612b3102ebb38c684c38bb");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _distributorsAppService.DeleteAsync(Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e"));

            // Assert
            var result = await _distributorRepository.FindAsync(c => c.Id == Guid.Parse("7e256683-82b8-4b08-81a1-a1b0e5b2057e"));

            result.ShouldBeNull();
        }
    }
}