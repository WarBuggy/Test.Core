using TestMDM.Distributors;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace TestMDM.EntityFrameworkCore;

[ConnectionStringName(TestMDMDbProperties.ConnectionStringName)]
public interface ITestMDMDbContext : IEfCoreDbContext
{
    DbSet<Distributor> Distributors { get; set; }
    DbSet<IdentityUser> IdentityUsers { get; set; }
    DbSet<DistributorIdentityUser> DistributorIdentityUsers { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}