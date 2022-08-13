using Inquiry.Distributors;
using Microsoft.EntityFrameworkCore;
using TestMDM;
using Volo.Abp;
using Volo.Abp.Identity;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Inquiry.EntityFrameworkCore;

public static class InquiryDbContextModelCreatingExtensions
{
    public static void ConfigureInquiry(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        /* Configure all entities here. Example:

        builder.Entity<Question>(b =>
        {
            //Configure table & schema name
            b.ToTable(InquiryDbProperties.DbTablePrefix + "Questions", InquiryDbProperties.DbSchema);

            b.ConfigureByConvention();

            //Properties
            b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

            //Relations
            b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

            //Indexes
            b.HasIndex(q => q.CreationTime);
        });
        */
        builder.Entity<Distributor>(b =>
        {
            b.ToTable(TestMDMDbProperties.DbTablePrefix + "Distributors", TestMDMDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.TenantId).HasColumnName(nameof(Distributor.TenantId));
            b.Property(x => x.CompanyName).HasColumnName(nameof(Distributor.CompanyName)).IsRequired().HasMaxLength(TestMDM.Distributors.DistributorConsts.CompanyNameMaxLength);
            b.Property(x => x.TaxId).HasColumnName(nameof(Distributor.TaxId)).IsRequired().HasMaxLength(TestMDM.Distributors.DistributorConsts.TaxIdMaxLength);
            b.HasMany(x => x.IdentityUsers).WithOne().HasForeignKey(x => x.DistributorId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<DistributorIdentityUser>(b =>
        {
            b.ToTable(InquiryDbProperties.DbTablePrefix + "DistributorIdentityUser" + InquiryDbProperties.DbSchema);
            b.ConfigureByConvention();

            b.HasKey(
            x => new { x.DistributorId, x.IdentityUserId }
            );

            b.HasOne<Distributor>().WithMany(x => x.IdentityUsers).HasForeignKey(x => x.DistributorId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.IdentityUserId).IsRequired().OnDelete(DeleteBehavior.NoAction);

            b.HasIndex(
                x => new { x.DistributorId, x.IdentityUserId }
            );
        });
    }
}
