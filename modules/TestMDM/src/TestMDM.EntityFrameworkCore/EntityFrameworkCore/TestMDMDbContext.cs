using TestMDM.Distributors;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace TestMDM.EntityFrameworkCore;

[ConnectionStringName(TestMDMDbProperties.ConnectionStringName)]
public class TestMDMDbContext : AbpDbContext<TestMDMDbContext>, ITestMDMDbContext
{
    public DbSet<Distributor> Distributors { get; set; }
    public DbSet<IdentityUser> IdentityUsers { get; set; }
    public DbSet<DistributorIdentityUser> DistributorIdentityUsers { get; set; }

    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public TestMDMDbContext(DbContextOptions<TestMDMDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ConfigureIdentityPro();
        builder.ConfigureTestMDM();
    }
}