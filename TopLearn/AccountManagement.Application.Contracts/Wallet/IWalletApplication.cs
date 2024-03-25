using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Wallet;

public interface IWalletApplication
{
    Task<Tuple<OperationResult, long>> ChargeWallet(ChargeWalletCommand command);
    Task<List<WalletViewModel>> WalletReports(long accountId);
    Task<WalletViewModel> GetWalletBy(long id);
    Task<OperationResult> SuccessPayment(long id);
}