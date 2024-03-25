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
            switch (result.Item1.Status)
            {
                case OperationResultStatus.Error:
                    ErrorAlert(result.Item1.Message);
                    return View(command);
                case OperationResultStatus.Success:
                    break;
                case OperationResultStatus.NotFound:
                    ErrorAlert(result.Item1.Message);
                    return View(command);
                default:
                    return NotFound();
            }

            #region Online Payment

            var amount = Convert.ToInt32(command.Amount);
            var email = _authenticationHelper.CurrentAccountEmail();
            var payment = new ZarinpalSandbox.Payment(amount);
            var callBackUrl = $"https://localhost:7135/CallBack?walletId={result.Item2}";

            var response = await payment.PaymentRequest("شارژ کیف پول",
                callBackUrl, email, "");

            if (response.Status == 100)
            {
               return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + response.Authority);
            }

            #endregion

            return BadRequest();
        }
    }
}
