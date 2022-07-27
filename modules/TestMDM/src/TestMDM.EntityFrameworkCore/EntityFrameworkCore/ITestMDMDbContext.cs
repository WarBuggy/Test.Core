using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace TestMDM.EntityFrameworkCore;

[ConnectionStringName(TestMDMDbProperties.ConnectionStringName)]
public interface ITestMDMDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
