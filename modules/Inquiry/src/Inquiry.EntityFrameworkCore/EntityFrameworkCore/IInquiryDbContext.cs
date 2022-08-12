using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Inquiry.EntityFrameworkCore;

[ConnectionStringName(InquiryDbProperties.ConnectionStringName)]
public interface IInquiryDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
