using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Wallet;

public interface IWalletApplication
{
    Task<OperationResult> ChargeWallet(ChargeWalletCommand command);
    Task<List<WalletViewModel>> WalletReports(long accountId);
}