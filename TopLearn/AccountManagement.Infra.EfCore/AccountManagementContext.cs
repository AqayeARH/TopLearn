using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.PermissionAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.WalletAgg;
using AccountManagement.Infra.EfCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infra.EfCore;

public class AccountManagementContext : DbContext
{
    public AccountManagementContext(DbContextOptions<AccountManagementContext> options) : base(options)
    {

    }

    #region Db Sets

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<WalletType> WalletTypes { get; set; }
    public DbSet<Wallet> Wallets { get; set; }

    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(AccountMapping).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}