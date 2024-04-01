using _0.Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Areas.Administrator.Pages.AccountManagement.Accounts
{
    public class IndexModel : PageModel
    {

        #region constructor injection

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        #endregion

        public List<AccountViewModel> Accounts { get; set; }
        public AccountSearchModel SearchModel { get; set; }

        public void OnGet(AccountSearchModel searchModel)
        {
            Accounts = _accountApplication.GetList(searchModel);
        }


        public async Task<IActionResult> OnGetCreate()
        {
            return Partial("Create", new CreateAccountCommand()
            {
                Roles = await _roleApplication.GetList()
            });
        }

        public async Task<IActionResult> OnPostCreate(CreateAccountCommand command, List<int> selectedRoles)
        {
            var result = await _accountApplication.Create(command, selectedRoles);


            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetActiveAccount(long id)
        {
            await _accountApplication.ActiveAccount(id);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnGetDeActiveAccount(long id)
        {
            await _accountApplication.DeActiveAccount(id);
            return RedirectToPage("Index");
        }

    }
}
