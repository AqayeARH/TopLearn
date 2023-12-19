using _0.Framework.Application;
using AccountManagement.Application.Contracts.Wallet;
using AccountManagement.Domain.WalletAgg;

namespace AccountManagement.Application;

public class WalletApplication : IWalletApplication
{
    #region constructor injection

    private readonly IWalletRepository _walletRepository;
    public WalletApplication(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    #endregion
    public async Task<OperationResult> ChargeWallet(ChargeWalletCommand command)
    {
        if (command.Amount == 0)
        {
            return OperationResult.Error("مبلغ وارد شده معتبر نمیباشد");
        }

        var wallet = new Wallet(WalletTypeId.In, command.AccountId, command.Amount,
            "شارژ کیف پول", command.IsPayed);

        await _walletRepository.Create(wallet);
        await _walletRepository.Save();

        return OperationResult.Success();
    }
    public async Task<List<WalletViewModel>> WalletReports(long accountId)
    {
        return await _walletRepository.WalletReports(accountId);
    }
}