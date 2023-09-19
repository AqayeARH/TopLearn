using AccountManagement.Common.Authentication;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Query.Contracts.UserPanel;

namespace TopLearn.Web.Areas.UserPanel.ViewComponents;

public class AccountInfoViewComponent : ViewComponent
{
    #region constructor injection

    private readonly IAccountQuery _accountQuery;
    private readonly IAuthenticationHelper _authenticationHelper;
    public AccountInfoViewComponent(IAccountQuery accountQuery, IAuthenticationHelper authenticationHelper)
    {
        _accountQuery = accountQuery;
        _authenticationHelper = authenticationHelper;
    }

    #endregion

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var email = _authenticationHelper.CurrentAccountEmail();
        var account = await _accountQuery.InformationAccount(email);

        return View("~/Areas/UserPanel/Views/Shared/Components/AccountInfo.cshtml", account);
    }
}