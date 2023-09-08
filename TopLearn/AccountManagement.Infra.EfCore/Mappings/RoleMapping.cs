using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mappings;

public class RoleMapping:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RoleTitle).IsRequired().HasMaxLength(100);

        #region Seed Data

        builder.HasData(
            new Role(1, "مدیر"),
            new Role(2, "مدرس"),
            new Role(3, "کاربر")
        );

        #endregion

        //Relations
        builder.HasMany(x => x.AccountRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId);
    }
}