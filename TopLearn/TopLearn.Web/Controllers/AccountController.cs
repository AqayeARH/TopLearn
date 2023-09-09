using _0.Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region constructor injection

        private readonly IAccountApplication _accountApplication;
        public AccountController(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        #endregion

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountRegisterCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _accountApplication.RegisterAccount(command);

            if (result.Item1.Status == OperationResultStatus.Success)
            {
                //TODO Send activate email
                SuccessAlert(result.Item1.Message);
                return View("SuccessRegister",result.Item2);
            }

            ErrorAlert(result.Item1.Message);
            return View(command);
        }

        #endregion
    }
}
