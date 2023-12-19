using AccountManagement.Application.Contracts.Wallet;
using AccountManagement.Common.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Areas.UserPanel.ViewComponents;

public class WalletReportViewComponent: ViewComponent
{

    #region constructor injection

    private readonly IWalletApplication _walletApplication;
    private readonly IAuthenticationHelper _authenticationHelper;
    public WalletReportViewComponent(IWalletApplication walletApplication, IAuthenticationHelper authenticationHelper)
    {
        _walletApplication = walletApplication;
        _authenticationHelper = authenticationHelper;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var accountId = _authenticationHelper.CurrentAccountId();
        var wallets = await _walletApplication.WalletReports(accountId);
        return View("~/Areas/UserPanel/Views/Shared/Components/WalletReport.cshtml", wallets);
    }
}