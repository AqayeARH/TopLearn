using AccountManagement.Domain.WalletAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mappings;

public class WalletTypeMapping:IEntityTypeConfiguration<WalletType>
{
    public void Configure(EntityTypeBuilder<WalletType> builder)
    {
        builder.ToTable("WalletTypes");
        builder.HasKey(x => x.TypeId);

        builder.Property(x => x.TypeId).ValueGeneratedNever();
        builder.Property(x => x.TypeTitle).IsRequired().HasMaxLength(50);

        //Relations

        builder.HasMany(x => x.Wallets)
            .WithOne(x => x.WalletType)
            .HasForeignKey(x => x.TypeId);
    }
}