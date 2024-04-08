using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TopLearn.Web.Areas.Administrator.Pages.AccountManagement.Roles
{
    public class IndexModel : PageModel
    {
        #region constructor injection

        private readonly IRoleApplication _roleApplication;
        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        #endregion

        public List<RoleViewModel> Roles { get; set; }

        public async Task OnGet()
        {
            Roles = await _roleApplication.GetList();
        }
    }
}
