using Inquiry.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Inquiry;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(InquiryEntityFrameworkCoreTestModule)
    )]
public class InquiryDomainTestModule : AbpModule
{

}
