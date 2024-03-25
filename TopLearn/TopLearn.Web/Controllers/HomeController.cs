using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _0.Framework.Application;
using AccountManagement.Application.Contracts.Wallet;
using TopLearn.Web.Models;

namespace TopLearn.Web.Controllers
{
    public class HomeController : BaseController
    {

        #region MyRegion

        private readonly IWalletApplication _walletApplication;
        public HomeController(IWalletApplication walletApplication)
        {
            _walletApplication = walletApplication;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("CallBack")]
        public async Task<IActionResult> CallBack(long walletId)
        {
            if (HttpContext.Request.Query["status"] != "" &&
                HttpContext.Request.Query["status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["authority"] != "" &&
                walletId != 0)
            {
                var authority = HttpContext.Request.Query["authority"];
                var wallet = await _walletApplication.GetWalletBy(walletId);
                var amount = Convert.ToInt32(wallet.Amount);
                var payment = new ZarinpalSandbox.Payment(amount);

                var response = await payment.Verification(authority);

                if (response.Status == 100)
                {
                    SuccessAlert("پرداخت با موفقیت انجام شد : کد رهگیری : " + response.RefId);
                    var result = await _walletApplication.SuccessPayment(walletId);

                    switch (result.Status)
                    {
                        case OperationResultStatus.Error:
                            break;
                        case OperationResultStatus.Success:
                            break;
                        case OperationResultStatus.NotFound:
                            ErrorAlert(result.Message);
                            break;
                        default:
                            return NotFound();
                    }
                }
                else
                {
                    ErrorAlert("خطا در پرداخت");
                }
            }
            else
            {
                ErrorAlert("خطا در پرداخت");
            }

            return Redirect("UserPanel/Wallet");
        }
    }
}