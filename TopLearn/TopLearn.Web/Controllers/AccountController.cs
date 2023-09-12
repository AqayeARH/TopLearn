﻿using _0.Framework.Application;
using _0.Framework.Application.Email;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;

namespace TopLearn.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region constructor injection

        private readonly IAccountApplication _accountApplication;
        private readonly IViewRenderService _viewRenderService;
        public AccountController(IAccountApplication accountApplication, IViewRenderService viewRenderService)
        {
            _accountApplication = accountApplication;
            _viewRenderService = viewRenderService;
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

                #region Send Active Email

                var emailBody = _viewRenderService.RenderToStringAsync("_ActivateAccountEmail", result.Item2);
                SendEmail.Send(result.Item2.Email, "فعالسازی حساب", emailBody);

                #endregion

                SuccessAlert(result.Item1.Message);
                return View("SuccessRegister", result.Item2);
            }

            ErrorAlert(result.Item1.Message);
            return View(command);
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginAccountCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            var result = await _accountApplication.LoginAccount(command);

            if (result.Status == OperationResultStatus.Success)
            {
                SuccessAlert(result.Message);
                return Redirect("/");
            }

            ErrorAlert(result.Message);
            return View(command);
        }

        #endregion

        #region Active Account

        [Route("ActiveAccount")]
        public async Task<IActionResult> ActiveAccount(string activeCode)
        {
            var result = await _accountApplication.ActiveAccount(activeCode);

            switch (result.Status)
            {
                case OperationResultStatus.Error:
                    ErrorAlert(result.Message);
                    break;
                case OperationResultStatus.Success:
                    SuccessAlert(result.Message);
                    return RedirectToAction("Login", "Account");
                case OperationResultStatus.NotFound:
                    ErrorAlert(result.Message);
                    break;
                default:
                    return NotFound();
            }

            return Redirect("/");
        }

        #endregion

        #region Logout

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountApplication.LogoutAccount();
            SuccessAlert(result.Message);
            return Redirect("/");
        }

        #endregion
    }
}
