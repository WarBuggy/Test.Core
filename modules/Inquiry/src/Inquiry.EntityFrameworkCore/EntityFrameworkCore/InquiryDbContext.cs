using Inquiry.Distributors;
using Microsoft.EntityFrameworkCore;
using TestMDM.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

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
        builder.ConfigureIdentityPro();
        builder.ConfigureTestMDM();
        builder.ConfigureInquiry();
    }
}
