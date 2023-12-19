using _0.Framework.Domain;
using AccountManagement.Application.Contracts.Wallet;

namespace AccountManagement.Domain.WalletAgg;

public interface IWalletRepository : IGenericRepository<long, Wallet>
{
    Task<List<WalletViewModel>> WalletReports(long accountId);
}