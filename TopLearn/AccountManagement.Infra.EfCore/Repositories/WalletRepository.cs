using _0.Framework.Infrastructure;
using AccountManagement.Domain.WalletAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infra.EfCore.Repositories;

public class WalletRepository:EfCoreGenericRepository<long,Wallet>,IWalletRepository
{
    #region constructor injection

    private readonly AccountManagementContext _context;

    public WalletRepository(AccountManagementContext context):base(context)
    {
        _context = context;
    }

    #endregion
}