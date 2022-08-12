using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Inquiry.EntityFrameworkCore;

[ConnectionStringName(InquiryDbProperties.ConnectionStringName)]
public class InquiryDbContext : AbpDbContext<InquiryDbContext>, IInquiryDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public InquiryDbContext(DbContextOptions<InquiryDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureInquiry();
    }
}
