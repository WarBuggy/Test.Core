using Inquiry.Distributors;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Inquiry.EntityFrameworkCore;

[ConnectionStringName(InquiryDbProperties.ConnectionStringName)]
public interface IInquiryDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
