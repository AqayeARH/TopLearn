using _0.Framework.Application;
using _0.Framework.Infrastructure;
using AccountManagement.Application.Contracts.Wallet;
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

    public async Task<List<WalletViewModel>> WalletReports(long accountId)
    {
        var wallets = await _context.Wallets
            .Where(x => x.AccountId == accountId && x.IsPay)
            .Select(x => new WalletViewModel()
            {
                Amount = x.Amount,
                CreationDate = x.CreationDate.ToFarsi(),
                Description = x.Description,
                IsPayed = x.IsPay,
                TypeId = x.TypeId
            }).ToListAsync();

        return wallets;
    }

    public async Task<WalletViewModel> GetWalletBy(long id)
    {
        var wallet = await _context.Wallets
            .Where(x => x.Id == id)
            .Select(x => new WalletViewModel()
            {
                Amount = x.Amount,
                CreationDate = x.CreationDate.ToFarsi(),
                Description = x.Description,
                IsPayed = x.IsPay,
                TypeId = x.TypeId,
                WalletId = x.Id,
            }).FirstOrDefaultAsync();

        return wallet;
    }
}