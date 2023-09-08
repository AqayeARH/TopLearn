using AccountManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mappings;

public class AccountRoleMapping:IEntityTypeConfiguration<AccountRole>
{
    public void Configure(EntityTypeBuilder<AccountRole> builder)
    {
        builder.ToTable("AccountRoles");

        builder.HasKey(x => new { x.RoleId, x.AccountId });

        //Relations

        builder.HasOne(x => x.Role)
            .WithMany(x => x.AccountRoles)
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Account)
            .WithMany(x => x.AccountRoles)
            .HasForeignKey(x => x.AccountId);
    }
}