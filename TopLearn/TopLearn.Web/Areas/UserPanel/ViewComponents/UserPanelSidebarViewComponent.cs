using AccountManagement.Application.Contracts.Account;
using AccountManagement.Common.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Areas.UserPanel.ViewComponents;

public class UserPanelSidebarViewComponent : ViewComponent
{
    #region constructor injection

    private readonly IAccountApplication _accountApplication;
    private readonly IAuthenticationHelper _authenticationHelper;
    public UserPanelSidebarViewComponent(IAccountApplication accountApplication, IAuthenticationHelper authenticationHelper)
    {
        _accountApplication = accountApplication;
        _authenticationHelper = authenticationHelper;
    }

    #endregion
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var email = _authenticationHelper.CurrentAccountEmail();
        var account = await _accountApplication.UserPanelSidebar(email);
        return View("~/Areas/UserPanel/Views/Shared/Components/UserPanelSidebar.cshtml", account);
    }
}