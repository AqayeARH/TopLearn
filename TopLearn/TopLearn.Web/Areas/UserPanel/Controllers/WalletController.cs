using _0.Framework.Application;
using AccountManagement.Application.Contracts.Wallet;
using AccountManagement.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Web.Controllers;

namespace TopLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : BaseController
    {
        #region constructor injection

        private readonly IWalletApplication _walletApplication;
        private readonly IAuthenticationHelper _authenticationHelper;
        public WalletController(IWalletApplication walletApplication, IAuthenticationHelper authenticationHelper)
        {
            _walletApplication = walletApplication;
            _authenticationHelper = authenticationHelper;
        }

        #endregion

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UserPanel/Wallet")]
        public async Task<IActionResult> Index(ChargeWalletCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            command.AccountId = _authenticationHelper.CurrentAccountId();
            command.IsPayed = false;

            var result = await _walletApplication.ChargeWallet(command);
            switch (result.Status)
            {
                case OperationResultStatus.Error:
                    ErrorAlert(result.Message);
                    return View(command);
                case OperationResultStatus.Success:
                    break;
                case OperationResultStatus.NotFound:
                    ErrorAlert(result.Message);
                    return View(command);
                default:
                    return NotFound();
            }

            return null;
        }
    }
}
