using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mappings;

public class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(500);
        builder.Property(x => x.ActiveCode).IsRequired().HasMaxLength(50);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(200);
        builder.Property(x => x.ImageName).IsRequired().HasMaxLength(200);

        //Relations
        builder.HasMany(x => x.AccountRoles)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId);
    }
}