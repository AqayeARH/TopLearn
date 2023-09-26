using _0.Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Query.Contracts.UserPanel;
using TopLearn.Web.Controllers;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : BaseController
    {
        #region constructor injection

        private readonly IAccountQuery _accountQuery;
        private readonly IAuthenticationHelper _authenticationHelper;
        private readonly IAccountApplication _accountApplication;
        public HomeController(IAccountQuery accountQuery, IAuthenticationHelper authenticationHelper,
            IAccountApplication accountApplication)
        {
            _accountQuery = accountQuery;
            _authenticationHelper = authenticationHelper;
            _accountApplication = accountApplication;
        }

        #endregion

        [Route("UserPanel")]
        public async Task<IActionResult> Index()
        {
            var email = _authenticationHelper.CurrentAccountEmail();
            var account = await _accountQuery.InformationAccount(email);
            return View(account);
        }

        [Route("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var accountId = _authenticationHelper.CurrentAccountId();
            var model = await _accountApplication.GetAccountForEditProfile(accountId);
            return View(model);
        }

        [HttpPost("UserPanel/EditProfile")]
        public async Task<IActionResult> EditProfile(EditProfileCommand command)
        {
            command.Id = _authenticationHelper.CurrentAccountId();

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _accountApplication.EditProfile(command);

            switch (result.Status)
            {
                case OperationResultStatus.Error:
                    ErrorAlert(result.Message);
                    break;
                case OperationResultStatus.Success:
                    SuccessAlert(result.Message);
                    _authenticationHelper.SignOut();
                    return Redirect("/");
                case OperationResultStatus.NotFound:
                    ErrorAlert(result.Message);
                    break;
                default:
                    return NotFound();
            }

            return View(command);
        }

        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("UserPanel/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            command.AccountId = _authenticationHelper.CurrentAccountId();

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _accountApplication.ChangePassword(command);

            switch (result.Status)
            {
                case OperationResultStatus.Error:
                    ErrorAlert(result.Message);
                    break;
                case OperationResultStatus.Success:
                    SuccessAlert(result.Message);
                    _authenticationHelper.SignOut();
                    return Redirect("/");
                case OperationResultStatus.NotFound:
                    ErrorAlert(result.Message);
                    break;
                default:
                    return NotFound();
            }

            return View(command);
        }
    }
}
