using AccountManagement.Domain.WalletAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infra.EfCore.Mappings;

public class WalletMapping:IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description).IsRequired().HasMaxLength(100);

        //Relations

        builder.HasOne(x => x.Account)
            .WithMany(x => x.Wallets)
            .HasForeignKey(x => x.AccountId);

        builder.HasOne(x => x.WalletType)
            .WithMany(x => x.Wallets)
            .HasForeignKey(x => x.TypeId);
    }
}