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
    public async Task<Tuple<OperationResult, long>> ChargeWallet(ChargeWalletCommand command)
    {
        if (command.Amount == 0)
        {
            return Tuple.Create(OperationResult.Error("مبلغ وارد شده معتبر نمیباشد"), Convert.ToInt64(0));
        }

        var wallet = new Wallet(WalletTypeId.In, command.AccountId, command.Amount,
            "شارژ کیف پول", command.IsPayed);

        await _walletRepository.Create(wallet);
        await _walletRepository.Save();

        return Tuple.Create(OperationResult.Success(), wallet.Id);
    }
    public async Task<List<WalletViewModel>> WalletReports(long accountId)
    {
        return await _walletRepository.WalletReports(accountId);
    }

    public async Task<WalletViewModel> GetWalletBy(long id)
    {
       return await _walletRepository.GetWalletBy(id);
    }

    public async Task<OperationResult> SuccessPayment(long id)
    {
        var wallet = await _walletRepository.Get(id);

        if (wallet == null)
        {
            return OperationResult.NotFound();
        }

        wallet.PaymentSuccess();
        await _walletRepository.Save();

        return OperationResult.Success();
    }
}